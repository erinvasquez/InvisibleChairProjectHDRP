using System;
/// <summary>
/// 12 Note chromatic scale starting
/// from A, written in sharps
/// BUT NOW IN BITS
/// A being the left most bit,
/// and GSHARP the right most
/// </summary>
[Flags] public enum SharpNoteBits {
    A = 1 << 11,
    ASHARP = 1 << 10,
    B = 1 << 9,
    C = 1 << 8,
    CSHARP = 1 << 7,
    D = 1 << 6,
    DSHARP = 1 << 5,
    E = 1 << 4,
    F = 1 << 3,
    FSHARP = 1 << 2,
    G = 1 << 1,
    GSHARP = 1 << 0
};
