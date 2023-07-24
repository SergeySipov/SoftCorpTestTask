using Application.Interfaces.Repositories.Common;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UserFeatures.Commands.Create;

public record CreateUserCommand : IRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IBaseRepository<User> _userBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordValidationService _passwordValidationService;

    public CreateUserCommandHandler(IBaseRepository<User> userBaseRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IPasswordValidationService passwordValidationService)
    {
        _userBaseRepository = userBaseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordValidationService = passwordValidationService;
    }

    public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordSalt = Guid.NewGuid().ToString();
        var passwordHash = _passwordValidationService.GeneratePasswordHash(request.Password, passwordSalt);

        var userEntity = _mapper.Map<User>(request);
        userEntity.PasswordHash = passwordHash;
        userEntity.PasswordSalt = passwordSalt;

        _userBaseRepository.Add(userEntity);
        return _unitOfWork.SaveAsync(cancellationToken);
    }
}