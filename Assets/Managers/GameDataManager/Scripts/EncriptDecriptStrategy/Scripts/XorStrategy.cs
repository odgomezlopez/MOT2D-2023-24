using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Encript Strategies/Xor", fileName = "XorStrategy")]
public class XorStrategy : EncriptDecriptStrategy
{
    public override string DecodeString(string source)
    {
        return EncryptDecrypt(source, key);
    }

    public override string EncodeString(string source)
    {
        return EncryptDecrypt(source, key);
    }

    protected string EncryptDecrypt(string szPlainText, string szEncryptionKey)
    {
        StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
        StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
        char Textch;
        for (int iCount = 0; iCount < szPlainText.Length; iCount++)
        {
            int stringIndex = iCount % szEncryptionKey.Length;
            Textch = szInputStringBuild[iCount];
            Textch = (char)(Textch ^ szEncryptionKey[stringIndex]);
            szOutStringBuild.Append(Textch);
        }

        return szOutStringBuild.ToString();
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(XorStrategy)),]
public class XorStrategyEditor : EncriptDecriptStrategyEditor
{

}
#endif
