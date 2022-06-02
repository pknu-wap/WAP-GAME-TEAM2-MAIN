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
                if (objArray.Length > 0)
                {
                    Assert.IsTrue(objArray.Length <= 1, string.Format("{0} shoud be only one, but you are trying to make it more than one", typeof(T).ToString()));
                    instance = objArray[0] as T;
                }
                else
                {
                    var newGameObj = new GameObject(typeof(T).ToString());
                    instance = newGameObj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
