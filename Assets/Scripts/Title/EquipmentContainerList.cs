namespace NTitle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NEquipment;


    public class EquipmentContainerList : MonoBehaviour
    {
        [SerializeField]
        private TitleMenu titleMenu;
        [SerializeField]
        private EquipmentDataList equipmentDataList;
        [SerializeField]
        private GameObject equipmentContainerPrefab;

        [SerializeField]
        private float alignX = 1;
        [SerializeField]
        private float alignY = 1;
        [SerializeField]
        private int column = 3;
        [SerializeField]
        private float containerSize = 1;

        private void Start()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float adjustedAlignX = alignX * (screenWidth / 1920f); // Assuming 1920 is the reference width
            float adjustedAlignY = alignY * (screenHeight / 1080f); // Assuming 1080 is the reference height

            for (int i = 0; i < equipmentDataList.list.Count; i++)
            {
                Vector3 pos = new Vector3((i % column) * adjustedAlignX, -(i / column) * adjustedAlignY, 0);
                GameObject container = Instantiate(equipmentContainerPrefab, this.transform.position + pos, Quaternion.identity, this.transform);
                container.transform.localScale = new Vector3(containerSize, containerSize, containerSize);
                container.GetComponent<TitleEquipmentContainer>().SetItem(equipmentDataList.list[i]);
                int temp = i;
                container.GetComponent<Button>().onClick.AddListener(() =>
                {
                    titleMenu.OnClickEquipButton(temp);
                });
            }
        }
    }

}
