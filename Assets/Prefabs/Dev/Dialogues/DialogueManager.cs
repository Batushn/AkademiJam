using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public UnityEngine.TextAsset textJSON;
    public Image image_left;
    public Image image_right;
    public TextMeshProUGUI name_left;
    public TextMeshProUGUI name_right;
    public TextMeshProUGUI textComponent;
    public string spritePath = "Sprites/";

    public float textAnimationSpeed; // 1 ile 100 arasýnda
    private int index = 0;
    private int totalDialogueCount;

    private Root jsonObject;
    private Dictionary<string, string> characterNamePositionMap = new();

    public class Character
    {
        public int CharacterIndex { get; set; }
        public string Name { get; set; }
        public string SpriteName { get; set; }
        public string Position { get; set; }
    }

    public class Dialogue
    {
        public string Name { get; set; }
        public string SpriteName { get; set; }
        public string Message { get; set; }
    }

    public class Root
    {
        public int DialogueIndex { get; set; }
        public string Audio { get; set; }
        public List<Character> Characters { get; set; }
        public List<Dialogue> Dialogues { get; set; }
    }


    void Start()
    {
        jsonObject = JsonConvert.DeserializeObject<Root>(textJSON.text);

        foreach (Character character in jsonObject.Characters)
        {
            characterNamePositionMap.Add(character.Name, character.Position);
            if ("left".Equals(character.Position.ToLower()))
            {
                name_left.text = character.Name;
                image_left.sprite = Resources.Load<Sprite>(spritePath + character.SpriteName);
            }
            else if ("right".Equals(character.Position.ToLower()))
            {
                name_right.text = character.Name;
                image_right.sprite = Resources.Load<Sprite>(spritePath + character.SpriteName);
            }
            else
            {
                Debug.LogError("ERROR: Invalid character position: " + character.Position);
            }
        }
        totalDialogueCount = jsonObject.Dialogues.Count;
        StartConversation();
    }

    void StartConversation()
    {
        gameObject.GetComponent<AudioSource>().Play();

        if (textAnimationSpeed == 0)
        {
            textAnimationSpeed = 1;
        }
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        GetMessageAndAdjustComponents();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == jsonObject.Dialogues[index].Message)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = jsonObject.Dialogues[index].Message;
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in jsonObject.Dialogues[index].Message.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(5 / textAnimationSpeed);
        }
    }

    void NextLine()
    {
        if (index < totalDialogueCount - 1)
        {
            index++;
            textComponent.text = string.Empty;
            GetMessageAndAdjustComponents();
        }
        else 
        {
            if (jsonObject.DialogueIndex == 7)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }

    }

    void GetMessageAndAdjustComponents()
    {
        string position = characterNamePositionMap.GetValueOrDefault(jsonObject.Dialogues[index].Name, string.Empty);
        if ("left".Equals(position.ToLower()))
        {
            if (jsonObject.Dialogues[index].SpriteName != null)
            {
                image_left.sprite = Resources.Load<Sprite>(spritePath + jsonObject.Dialogues[index].SpriteName);
            }
            image_left.gameObject.SetActive(true);
            name_left.gameObject.SetActive(true);
            image_right.gameObject.SetActive(false);
            name_right.gameObject.SetActive(false);
        }
        else if ("right".Equals(position.ToLower()))
        {
            if (jsonObject.Dialogues[index].SpriteName != null)
            {
                image_right.sprite = Resources.Load<Sprite>(spritePath + jsonObject.Dialogues[index].SpriteName);
            }
            image_right.gameObject.SetActive(true);
            name_right.gameObject.SetActive(true);
            image_left.gameObject.SetActive(false);
            name_left.gameObject.SetActive(false);
        }
        else
        {
            image_right.gameObject.SetActive(false);
            name_right.gameObject.SetActive(false);
            image_left.gameObject.SetActive(false);
            name_left.gameObject.SetActive(false);
        }

        StartCoroutine(TypeLine());
    }
}
