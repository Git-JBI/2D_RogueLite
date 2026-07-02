using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    [SerializeField] private Image _gradeImage;
    [SerializeField] private TextMeshProUGUI _descText;

    public virtual void Init(EquipmentItemData data, string desc)
    {
        SetGradeColor((GameManager.Instance.ResourceManager.GetScriptable("GameDatas/RarityImageColorData/RarityImageColorData") as SO_RarityImageColorData).Colors[(int)data.Rarity]);
        SetDescText(desc);
    }

    private void SetGradeColor(Color32 gradeColor)
    {
        _gradeImage.color = gradeColor;
    }

    private void SetDescText(string desc)
    {
        _descText.text = desc;
    }
}
