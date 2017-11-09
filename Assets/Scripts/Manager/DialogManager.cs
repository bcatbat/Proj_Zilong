using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("mainAssets")]
    public GameObject MainPanel;  // 对话框
    public GameObject TitlePanel;
    public GameObject DialogPanel;
    [Header("subAssets")]
    public Text title;      // 标题
    public Image icon;     // 头像
    public Text role;       // 角色名称
    public Text content;    // 对话内容

    [Header("Parameter")]
    public float tickTime = .1f; // 打字间隔
    private float titleLifeTime = 2.0f; // 标题栏显示时间

    // 输出段落. 来自进度
    private IEnumerator PrintParagraph(StoryProgress sp)
    {
        // 防空
        if (GlobalInfo.progress == null)
            GlobalInfo.progress = new StoryProgress();

        var dialogEnum = StoryDataProcessor.GetCurrentMainParagraph(sp);
        if (dialogEnum == null)
            Debug.LogError("传入的进度有误!");

        MainPanel.SetActive(true);
        DialogPanel.SetActive(false);
        // 每逢开头, 自动输出标题. 若不是标题, 就跳过
        if (GlobalInfo.progress.progressParagraph == 0)
            yield return PrintTitle();
        // 显示对话框
        DialogPanel.SetActive(true);
        Debug.Log("显示框打开时间:" + Time.time);
        yield return PrintDialogEnum(dialogEnum);
        DialogPanel.SetActive(false);
        MainPanel.SetActive(false);
        //StoryDataProcessor.StoryForward();// 结束以后, 剧情推进到下一步.
    }

    // 输出段落. 来自dialog流
    private IEnumerator PrintDialogEnum(IEnumerable<Dialog> dialogs)
    {
        // 输出段落正文
        foreach(var d in dialogs)
        {
            yield return PrintDialog(d);
            yield return WaitForClick();
        }
    }

    // 设置对话内容. 输出一句话. 
    private IEnumerator PrintDialog(Dialog dialog)
    {
        this.role.text = dialog.Role;        
        this.icon.sprite = StoryDataProcessor.LoadSpriteFromAssetBundle(dialog.Role);
        yield return TickingPrint(dialog.Content);        
    }

    // 设置标题内容
    private IEnumerator PrintTitle()
    {
        TitlePanel.SetActive(true);
        string titleText = StoryDataProcessor.GetTitleOfEpisode();
        title.text = titleText;
        // 持续一段时间
        yield return new WaitForSeconds(titleLifeTime);
        this.title.text = "";   // 清空
        TitlePanel.SetActive(false);
    }

    // 打字机式显示
    private IEnumerator TickingPrint(string content)
    {
        this.content.text = ""; // 清空内容框

        List<char> splitcontent = new List<char>();
        splitcontent = content.ToList();
        
        foreach(var c in splitcontent)
        {
            this.content.text += c;
            yield return new WaitForSecondsUnlessClicked(tickTime, "Fire1");
            if (IsClicked())
                break;
        }
        this.content.text = content;
        Debug.Log("over");
        yield return new WaitForSeconds(tickTime);
    }    

    // 等待点击继续
    private IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() =>
        {
            bool isClicked = false;
            isClicked = Input.GetButtonDown("Fire1");
            //Debug.Log("等待鼠标" + isClicked);
            return isClicked;
        });
        //Debug.Log(Time.time.ToString("f2"));
        yield return new WaitForSeconds(tickTime); // 下一句会连击...
    }

    // 检测点击
    private bool IsClicked()
    {
        bool isClicked = false;
        isClicked = Input.GetButtonDown("Fire1");        
        return isClicked;
    }

    #region SimpleTest
    private void Start()
    {
        // 第一章
        GlobalInfo.progress = new StoryProgress(1,0,0);

        StartCoroutine(PrintParagraph(GlobalInfo.progress));
        
    }
    #endregion

}
