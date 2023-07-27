using Application.Common.Models.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using MediatR;
using System.Security.Authentication;

namespace Application.UseCases.UserFeatures.Commands.RefreshAccessToken;

public record RefreshAccessTokenCommand : IRequest<LoginResponseModel>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

internal class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, LoginResponseModel>
{
    private readonly ITokenGenerationService _tokenGenerationService;
    private readonly IBaseRepository<UserRefreshToken> _userRefreshTokenBaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

    public RefreshAccessTokenCommandHandler(ITokenGenerationService tokenGenerationService, 
        IBaseRepository<UserRefreshToken> userRefreshTokenBaseRepository, 
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IUserRefreshTokenRepository userRefreshTokenRepository)
    {
        _tokenGenerationService = tokenGenerationService;
        _userRefreshTokenBaseRepository = userRefreshTokenBaseRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userRefreshTokenRepository = userRefreshTokenRepository;
    }

    public async Task<LoginResponseModel> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userClaims = _tokenGenerationService.GetUserClaimsFromToken(request.AccessToken);

        var userLoginData = await _userRepository.GetUserLoginDataAsync(userClaims.UserEmail);
        if (userLoginData == null)
        {
            throw new AuthenticationException();
        }

        var userRefreshToken = await _userRefreshTokenRepository.GetUserRefreshTokenAsync(Guid.Parse(request.RefreshToken));
        if (userRefreshToken == null || userRefreshToken.ExpirationDateTime <= DateTime.Now)
        {
            throw new AuthenticationException();
        }

        var userTokens = _tokenGenerationService.GenerateAccessAndRefreshTokens(userLoginData.Id,
            userLoginData.Email);

        userRefreshToken.Token = userTokens.RefreshToken.Token;
        userRefreshToken.ExpirationDateTime = userTokens.RefreshToken.ExpirationDateTime;
        await _userRefreshTokenBaseRepository.UpdateAsync(userRefreshToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new LoginResponseModel
        {
            Username = userLoginData.Username,
            AccessToken = userTokens.AccessJwtToken,
            RefreshToken = userTokens.RefreshToken.Token.ToString()
        };
    }
}