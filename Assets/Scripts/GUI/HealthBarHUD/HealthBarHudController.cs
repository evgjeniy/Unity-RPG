using Entities;

namespace GUI.HealthBarHUD
{
    [System.Serializable]
    public class HealthBarHudController
    {
        public HealthBarView View;

        public void UpdateHealth(EntityState state)
        {
            View.fillImage.color = View.gradient.Evaluate((float) state.CurrentHealth / state.MaxHealth);
            View.slider.value = (float) state.CurrentHealth / state.MaxHealth;
            View.text.text = $"{state.CurrentHealth} / {state.MaxHealth}";
        }
    }
}