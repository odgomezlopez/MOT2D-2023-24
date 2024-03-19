using Newtonsoft.Json.Linq;
using UnityEngine;

public interface ISaveable
{
    JToken CaptureAsJToken();
    void RestoreFromJToken(JToken state);
}
