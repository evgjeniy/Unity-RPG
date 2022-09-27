namespace GUI.DescriptionHUD
{
    [System.Serializable]
    public class DescriptionHudController
    {
        public DescriptionView View;

        public void SetText(string description)
        {
            View.Description.text = description;
        }
    }
}