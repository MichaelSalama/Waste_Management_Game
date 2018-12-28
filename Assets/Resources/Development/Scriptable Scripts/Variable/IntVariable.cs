using UnityEngine;

[CreateAssetMenu(fileName ="Int", menuName = "Raskulls/Variable/Int", order = 1)]
public class IntVariable : ScriptableObject
{
    public int value;

    public void SetValue(int val)
    {
        value = val;
    }
}
