using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int numHearts;
    [SerializeField] private Image[] heartpngs;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite halfHeart;

    private void Start()
    {
        var playerHealth = Player.Instance.HealthManager;
        playerHealth.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(object sender, HealthEventArgs e)
    {
        UpdateHealth(e.health);
    }

    public void UpdateHealth(float health)
    {
        for (int i = 0; i < heartpngs.Length; i++)
        {
            if (i < Mathf.Floor(health / 10))
            {
                heartpngs[i].sprite = fullHeart;
            }
            else if (i < Mathf.Floor((health + 5) / 10))
            {
                heartpngs[i].sprite = halfHeart;
            }
            else
            {
                heartpngs[i].sprite = emptyHeart;
            }
            if (i < numHearts)
            {
                heartpngs[1].enabled = true;
            }
            else
            {
                heartpngs[1].enabled = false;
            }
        }
    }
}
