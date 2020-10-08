
using UnityEngine;
using UnityEngine.UI;

public class I_UI : MonoBehaviour
{
    protected GameObject DrawUIObject;


    public virtual void Draw(bool isVisable) 
    {
        DrawUIObject.SetActive(isVisable);
    }

    public virtual void Setting(GameObject obj) {
        DrawUIObject = obj;
    }
}
