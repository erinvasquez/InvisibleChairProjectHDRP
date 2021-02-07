
/// <summary>
/// An Integer Vector2 struct
/// used to store X Z coordinates
/// </summary>
[System.Serializable]
public struct IntVector2{

    public int x, z;

    public IntVector2(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
        a.x += b.x;
        a.z += b.z;

        return a;
    }

    

}
