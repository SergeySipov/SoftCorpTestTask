using System.Security.Authentication;
using Application.Common.Models.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UserFeatures.Commands.AuthorizeUserAndCreateTokens;

public record AuthenticateUserAndGetJwtTokenCommand : IRequest<LoginResponseModel>
{
    public string Username { get; init; }
    public string Password { get; init; }
}

internal class AuthenticateUserAndGetJwtTokenCommandHandler : IRequestHandler<AuthenticateUserAndGetJwtTokenCommand,
    LoginResponseModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordValidationService _passwordValidationService;
    private readonly ITokenGenerationService _tokenGeneratorService;
    private readonly IBaseRepository<UserRefreshToken> _userRefreshTokenBaseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticateUserAndGetJwtTokenCommandHandler(IUserRepository userRepository,
        IPasswordValidationService passwordValidationService,
        ITokenGenerationService tokenGeneratorService,
        IBaseRepository<UserRefreshToken> userRefreshTokenBaseRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordValidationService = passwordValidationService;
        _tokenGeneratorService = tokenGeneratorService;
        _userRefreshTokenBaseRepository = userRefreshTokenBaseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseModel> Handle(AuthenticateUserAndGetJwtTokenCommand request,
        CancellationToken cancellationToken)//TODO возможно стоит разбить на отдельные command и query
    {
        var userLoginData = await _userRepository.GetUserLoginDataAsync(request.Username);
        if (userLoginData == null)
        {
            throw new AuthenticationException("Wrong login or password");
        }

        var isPasswordValid = _passwordValidationService.ValidatePassword(
            request.Password,
            userLoginData.PasswordHash,
            userLoginData.PasswordSalt);

        if (!isPasswordValid)
        {
            throw new AuthenticationException("Wrong login or password");
        }

        var userTokens = _tokenGeneratorService.GenerateAccessAndRefreshTokens(userLoginData.Id,
            userLoginData.Email);

        var userRefreshToken = _mapper.Map<UserRefreshToken>(userTokens.RefreshToken);
        _userRefreshTokenBaseRepository.Add(userRefreshToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new LoginResponseModel
        {
            Username = userLoginData.Username,
            AccessToken = userTokens.AccessJwtToken,
            RefreshToken = userTokens.RefreshToken.Token.ToString()
        };
    }
}