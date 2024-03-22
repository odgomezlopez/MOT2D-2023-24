using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Encript Strategies/XorBase", fileName = "XorBaseStrategy")]
public class XorBaseStrategy : XorStrategy
{
    public override string DecodeString(string source)
    {
        return EncryptDecrypt(DecodeFromBase64String(source), key);
    }

    public override string EncodeString(string source)
    {
        return EncodeAsBase64String(EncryptDecrypt(source, key));
    }

    string EncodeAsBase64String(string source)
    {
        byte[] sourceArray = new byte[source.Length];

        for (int i = 0; i < source.Length; i++)
            sourceArray[i] = (byte)source[i];

        return Convert.ToBase64String(sourceArray);
    }

    string DecodeFromBase64String(string source)
    {
        byte[] sourceArray = Convert.FromBase64String(source);
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < sourceArray.Length; i++)
            builder.Append((char)sourceArray[i]);

        return builder.ToString();
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(XorBaseStrategy)),]
public class XorBaseStrategyEditor : EncriptDecriptStrategyEditor
{

}
#endif
