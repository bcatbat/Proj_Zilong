using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItem : Item {
    public string testName;

    public TaskItem()
    {
        testName = "Task";
        itemType = ItemType.task;
    }
    
    // 一些特殊的效果
}
