using UnityEngine;

[CreateAssetMenu(fileName = "float", menuName = "Raskulls/Variable/float", order = 2)]
public class FloatVariable : ScriptableObject
{
    public float value;

    public void SetValue(int val)
    {
        value = val;
    }
}
