
public static string GetRandomResource(string resourceType)
{
    var dict = new Dictionary<string, List<VersionRange>>{
        { "os", new List<VersionRange> {
            new VersionRange("Windows NT 6.4"),
            new VersionRange("Windows NT 10.0; Win64; x64")
        }},
        { "ChromeWebkitVersion", new List<VersionRange> {
            new VersionRange(new Version(528, 0, 0, 0), new Version(532, 0, 0, 0)),
            new VersionRange(new Version(530, 0, 0, 0), new Version(534, 0, 0, 0))
        }},
        { "ChromeVersion", new List<VersionRange> {
            new VersionRange(new Version(92, 0, 4515, 159), new Version(93, 0, 0, 0)),
            new VersionRange(new Version(92, 0, 4515, 131), new Version(93, 0, 0, 0))
        }},
        { "AndroidVersion", new List<VersionRange> {
            new VersionRange(new Version(7, 0, 0, 0), new Version(8, 0, 0, 0)),
            new VersionRange(new Version(8, 1, 0, 0), new Version(9, 0, 0, 0))
        }},
        { "MacOSVersion", new List<VersionRange> {
            new VersionRange(new Version(10, 15, 0, 0), new Version(11, 6, 0, 0)),
            new VersionRange(new Version(12, 0, 0, 0), new Version(13, 0, 0, 0))
        }},
        { "Device", new List<VersionRange> {
            new VersionRange("RMX1911"),
            new VersionRange("Redmi 3 Build/QQ1D.200205.002")
        }}
    };
    var versionRange = dict[resourceType][randomInstance.Next(0, dict[resourceType].Count)];
    switch (resourceType)
    {
        case "os":
        case "Device":
            return versionRange.Value;
        default:
            return versionRange.GetRandomVersion().ToString();
    }
}

public class VersionRange
{
    private readonly Version _minVersion;
    private readonly Version _maxVersion;

    public string Value => _minVersion == _maxVersion ? _minVersion.ToString() : (_minVersion + "-" + _maxVersion);

    public VersionRange(string versionString) : this(Version.Parse(versionString)) { }

    public VersionRange(Version version) : this(version, version) { }

    public VersionRange(Version minVersion, Version maxVersion)
    {
        _minVersion = minVersion ?? throw new ArgumentNullException(nameof(minVersion));
        _maxVersion = maxVersion ?? throw new ArgumentNullException(nameof(maxVersion));
    }

    public bool Contains(Version version)
    {
        if (version == null) throw new ArgumentNullException(nameof(version));
        return _minVersion <= version && _maxVersion >= version;
    }

    public Version GetRandomVersion()
    {
        var major = randomInstance.Next(_minVersion.Major, _maxVersion.Major + 1);
        var minor = randomInstance.Next(major == _minVersion.Major ? _minVersion.Minor : 0, major == _maxVersion.Major ? _maxVersion.Minor + 1 : 13);
        var build = randomInstance.Next(minor == _minVersion.Minor && major == _minVersion.Major ? _minVersion.Build : 0, minor == _maxVersion.Minor && major == _maxVersion.Major ? _maxVersion.Build + 1 : 100);
        var revision = randomInstance.Next(build == _minVersion.Build && minor == _minVersion.Minor && major == _minVersion.Major ? _minVersion.Revision : 0, build == _maxVersion.Build && minor == _maxVersion.Minor && major == _maxVersion.Major ? _maxVersion.Revision + 1 : 10);
        return new Version(major, minor, build, revision);
    }
}

public enum Platform
{
    Desktop,
    Mobile
}
