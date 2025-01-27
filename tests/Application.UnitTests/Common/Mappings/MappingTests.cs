using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using FileManager.Application.Common.Interfaces;
using FileManager.Application.Common.Models;
using FileManager.Application.Files.Queries.GetFileDetails;
using FileManager.Application.Files.Queries.GetFilesWithFiltersQuery;
using FileManager.Domain.Entities;
using NUnit.Framework;

namespace FileManager.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(FileDto), typeof(FileDto))]
    [TestCase(typeof(FileDetailsDto), typeof(FileDetailsDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
