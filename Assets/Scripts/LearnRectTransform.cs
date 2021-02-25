using UnityEngine;

public class LearnRectTransform : MonoBehaviour
{
    RectTransform m_RectTransform;

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();

        Vector3[] v = new Vector3[4];
        m_RectTransform.GetLocalCorners(v);
        Debug.Log("Local Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("Local Corner " + i + " : " + v[i]);
        }
    }

    private void Update()
    {
        if(Input.GetButton("w"))
        {
            // m_RectTransform.anchoredPosition = m_RectTransform.anchoredPosition + new Vector2(0, 1);
            // m_RectTransform.anchorMax = m_RectTransform.anchorMax + new Vector2(0, 0.01f);
            // m_RectTransform.localPosition = new Vector3(0, 100, 0);
            // m_RectTransform.anchoredPosition = new Vector2(0, 100);
            m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            // m_RectTransform.sizeDelta = new Vector2(200, 300);
        }
        else if(Input.GetButton("s"))
        {
            // m_RectTransform.anchoredPosition = m_RectTransform.anchoredPosition + new Vector2(0, -1);
            // m_RectTransform.anchorMax = m_RectTransform.anchorMax + new Vector2(0, -0.01f);
            // m_RectTransform.localPosition = new Vector3(0, -100, 0);
            // m_RectTransform.anchoredPosition = new Vector2(0, -100);
            m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            // m_RectTransform.sizeDelta = new Vector2(200, 100);
        }
        else if (Input.GetButton("a"))
        {
            // m_RectTransform.anchoredPosition = m_RectTransform.anchoredPosition + new Vector2(-1, 0);
            // m_RectTransform.anchorMax = m_RectTransform.anchorMax + new Vector2(-0.01f, 0);
            // m_RectTransform.localPosition = new Vector3(-100, 0, 0);
            // m_RectTransform.anchoredPosition = new Vector2(-100, 0);
            m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            // m_RectTransform.sizeDelta = new Vector2(100, 200);
        }
        else if (Input.GetButton("d"))
        {
            // m_RectTransform.anchoredPosition = m_RectTransform.anchoredPosition + new Vector2(1, 0);
            // m_RectTransform.anchorMax = m_RectTransform.anchorMax + new Vector2(0.01f, 0);
            // m_RectTransform.localPosition = new Vector3(100, 0, 0);
            // m_RectTransform.anchoredPosition = new Vector2(100, 0);
            m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
            // m_RectTransform.sizeDelta = new Vector2(300, 200);
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        //The Labels show what the Sliders represent
        GUI.Label(new Rect(0, 0, 500, 50), "Anchor Position X : " + m_RectTransform.anchoredPosition.x.ToString(), style);
        GUI.Label(new Rect(0, 50, 500, 50), "Anchor Position Y : " + m_RectTransform.anchoredPosition.y.ToString(), style);
        GUI.Label(new Rect(0, 100, 500, 50), "AnchorMax : " + m_RectTransform.anchorMax.ToString(), style);
        GUI.Label(new Rect(0, 150, 500, 50), "AnchorMin : " + m_RectTransform.anchorMin.ToString(), style);
        GUI.Label(new Rect(0, 200, 500, 50), "Left : " + m_RectTransform.offsetMin.x.ToString() + " Bottom : " + m_RectTransform.offsetMin.y.ToString(), style);
        GUI.Label(new Rect(0, 250, 500, 50), "Right : " + m_RectTransform.offsetMax.x.ToString() + " Top : " + m_RectTransform.offsetMax.y.ToString(), style);
        GUI.Label(new Rect(0, 300, 500, 50), "Pivot :" + m_RectTransform.pivot.ToString(), style);
        GUI.Label(new Rect(0, 350, 500, 50), "Rect : " + m_RectTransform.rect, style);
        GUI.Label(new Rect(0, 400, 500, 50), "SizeDelta : " + m_RectTransform.sizeDelta, style);
        GUI.Label(new Rect(0, 450, 500, 50), "localPosition:" + m_RectTransform.localPosition.ToString(), style);
    }
}