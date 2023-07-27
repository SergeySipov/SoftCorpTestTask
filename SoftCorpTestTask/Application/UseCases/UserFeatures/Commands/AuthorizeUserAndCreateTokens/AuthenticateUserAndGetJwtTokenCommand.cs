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
    public string Email { get; init; }
    public string Password { get; init; }
}

internal class AuthenticateUserAndGetJwtTokenCommandHandler : IRequestHandler<AuthenticateUserAndGetJwtTokenCommand,
    LoginResponseModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordValidationService _passwordValidationService;
    private readonly ITokenGenerationService _tokenGenerationService;
    private readonly IBaseRepository<UserRefreshToken> _userRefreshTokenBaseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

    public AuthenticateUserAndGetJwtTokenCommandHandler(IUserRepository userRepository,
        IPasswordValidationService passwordValidationService,
        ITokenGenerationService tokenGenerationService,
        IBaseRepository<UserRefreshToken> userRefreshTokenBaseRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork, 
        IUserRefreshTokenRepository userRefreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordValidationService = passwordValidationService;
        _tokenGenerationService = tokenGenerationService;
        _userRefreshTokenBaseRepository = userRefreshTokenBaseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRefreshTokenRepository = userRefreshTokenRepository;
    }

    public async Task<LoginResponseModel> Handle(AuthenticateUserAndGetJwtTokenCommand request,
        CancellationToken cancellationToken)
    {
        var userLoginData = await _userRepository.GetUserLoginDataAsync(request.Email);
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

        var userTokens = _tokenGenerationService.GenerateAccessAndRefreshTokens(userLoginData.Id,
            userLoginData.Email);

        var oldUserRefreshToken = await _userRefreshTokenRepository.GetUserRefreshTokenAsync(userLoginData.Id);
        if (oldUserRefreshToken != null)
        {
            _userRefreshTokenBaseRepository.Delete(oldUserRefreshToken);
        }

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