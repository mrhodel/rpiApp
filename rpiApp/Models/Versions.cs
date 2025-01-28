/*
 * Versions.cs
 * 1/28/2025 Mike Hodel
*/
using Semver;
using System.Reflection;

namespace rpiApp.Models;

/// <summary>
/// Ref: https://semver.org
/// Short summary of SemVer: https://www.geeksforgeeks.org/introduction-semantic-versioning/
/// </summary>
/// 

public static class Versions
{
    public static SemVersion? CurrentVersion { get; } = SemVersion.ParsedFrom(0, 1, 0, "rc.1");
    public static string? ApplicationName { get; } = Assembly.GetEntryAssembly()?.GetName().Name;
}
