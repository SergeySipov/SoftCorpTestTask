using Application.Interfaces.DbContextSeed;
using Application.Interfaces.Services;
using Bogus;
using Domain.Entities;
using Infrastructure.AppSettings;
using Microsoft.Extensions.Options;
using Persistence.DbContexts;

namespace Persistence.DbContextSeed;

public class SoftCorpTestTaskDbContextSeed : IDbContextSeed
{
    private readonly IPasswordValidationService _passwordValidationService;
    private readonly DataGeneratorSettings _dataGeneratorSettings;
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public SoftCorpTestTaskDbContextSeed(IPasswordValidationService passwordValidationService, 
        IOptions<DataGeneratorSettings> dataGeneratorSettings, 
        SoftCorpTestTaskDbContext dbContext)
    {
        _passwordValidationService = passwordValidationService;
        _dbContext = dbContext;
        _dataGeneratorSettings = dataGeneratorSettings.Value;
    }

    public void InitDbWithDefaultValues()
    {
        if (!_dbContext.Database.CanConnect())
        {
            return;
        }

        if (!_dbContext.Users.Any())
        {
            var users = GenerateFakeUsers();

            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();
        }

        if (!_dbContext.Families.Any())
        {
            var families = GenerateFakeFamilies();

            _dbContext.Families.AddRange(families);
            _dbContext.SaveChanges();
        }

        if (!_dbContext.UsersFamilies.Any())
        {
            var userFamilies = GenerateFakeUserFamilies();

            _dbContext.UsersFamilies.AddRange(userFamilies);
            _dbContext.SaveChanges();
        }
    }

    private IEnumerable<Family> GenerateFakeFamilies()
    {
        var familyFaker = new Faker<Family>()
            .RuleFor(f => f.Title, f => f.Name.LastName());

        var a = familyFaker.Generate(_dataGeneratorSettings.NumbersOfFamiliesToGenerate);

        return a;
    }

    private IEnumerable<User> GenerateFakeUsers()
    {
        var uniqueValue = 0;

        var passwordSalt = Guid.NewGuid().ToString();
        var passwordHash = _passwordValidationService.GeneratePasswordHash(_dataGeneratorSettings.DefaultUserPassword, passwordSalt);
        var userFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Username, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Email, (f, u) => $"{++uniqueValue}{f.Internet.Email(u.FirstName, u.LastName)}")
            .RuleFor(u => u.PasswordHash, _ => passwordHash)
            .RuleFor(u => u.PasswordSalt, _ => passwordSalt);
        
        return userFaker.Generate(_dataGeneratorSettings.NumbersOfUsersToGenerate);
    }

    private IEnumerable<UserFamily> GenerateFakeUserFamilies()
    {
        var rand = new Random();

        var result = new List<UserFamily>(_dataGeneratorSettings.NumbersOfUsersToGenerate);
        for (var i = 1; i <= _dataGeneratorSettings.NumbersOfUsersToGenerate; i++)
        {
            var newUserFamily = new UserFamily
            {
                UserId = i,
                FamilyId = rand.Next(1, _dataGeneratorSettings.NumbersOfFamiliesToGenerate)
            };

            result.Add(newUserFamily);
        }

        return result;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}