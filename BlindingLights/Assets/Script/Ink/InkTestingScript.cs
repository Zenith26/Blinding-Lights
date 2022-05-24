using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InkTestingScript : MonoBehaviour
{
  public TextAsset inkJSON;
  private Story story;
  private int storyindex;

  public TMP_Text textPrefab;
  public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
      story = new Story(inkJSON.text);

      refreshUI();
    }

    void refreshUI()
    {
      eraseUI();

      TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;
      storyText.text = loadStoryChunk();
      storyText.transform.SetParent(this.transform, false);

      foreach(Choice choice in story.currentChoices)
      {
        Button choiceButton = Instantiate(buttonPrefab) as Button;
        TMP_Text choiceText = buttonPrefab.GetComponentInChildren<TMP_Text>();
        choiceText.text = choice.text;
        choiceButton.transform.SetParent(this.transform, false);

        choiceButton.onClick.AddListener(delegate{
          chooseStoryChoice(choice);
        });
      }
    }

    void eraseUI()
    {
      for(int i = 0; i < this.transform.childCount; i++)
      {
        Destroy(this.transform.GetChild(i).gameObject);
      }
    }

    void chooseStoryChoice(Choice choice)
    {
      Debug.Log(choice);
      story.ChooseChoiceIndex(choice.index);
      refreshUI();

      storyindex++;
      if (storyindex==6)
      {
        SceneManager.LoadScene(2);
      }
    }

    // Update is called once per frame
    void Update()
    {

    }

    string loadStoryChunk()
    {
      string text = "";

      if(story.canContinue)
      {
        text = story.ContinueMaximally();
      }


      return text;
    }
}
