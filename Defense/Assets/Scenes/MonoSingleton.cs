using UnityEngine;
using UnityEngine.Assertions;

public class MonoSingleton<T> : MonoBehaviour where T:  MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                var objArray = Resources.FindObjectsOfTypeAll(typeof(T));
                Assert.IsTrue(objArray.Length >= 1, string.Format("{0} shoud be only one, but you are trying to make it more than one", typeof(T).ToString()));
                instance = objArray[0] as T;
            }

            return instance;
        }
    }
}
