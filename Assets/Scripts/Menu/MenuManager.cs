using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(nameof(InitialLogo));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }

    public float t;
    [SerializeField] private Image initialImage;
    [SerializeField] private GameObject menu;

    private IEnumerator InitialLogo()
    {
        while (initialImage.fillAmount > 0f)
        {
            initialImage.fillAmount -= Time.deltaTime / 3.7f;
            if (initialImage.fillAmount <= 0)
            {
                initialImage.gameObject.SetActive(false);
                StopCoroutine(nameof(InitialLogo));
            }
        }
        yield return new WaitForFixedUpdate();
    } 
}
