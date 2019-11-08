# HW9

### 1. 血条（Health Bar）的预制设计

**具体要求如下**

- 分别使用 IMGUI 和 UGUI 实现
- 使用 UGUI，血条是游戏对象的一个子元素，任何时候需要面对主摄像机
- 分析两种实现的优缺点
- 给出预制的使用方法



**IMGUI**

> 使用一个红色的 **cube** 作为血条，并设置其跟随任务（Ethan）移动。而血条的减少，我是通过改变其localScale来是实现的。在 **cube** 上挂载一个脚本，并检测点击事件，检测到鼠标点击时， **cube** 的长度减少 **0.1** 这样就做成了血条减少的效果。
>
> **具体代码如下：**
>
> ```c#
> using System.Collections;
> using System.Collections.Generic;
> using UnityEngine;
> 
> public class blood : MonoBehaviour
> {
>     float padding = 0;
>     public Transform t;
>     // Start is called before the first frame update
>     void Start()
>     {
>         
>     }
> 
>     // Update is called once per frame
>     void Update()
>     {
>         this.transform.position = new Vector3(t.position.x-padding, t.position.y + 2, t.position.z);
>     }
>     private void OnGUI()
>     {
>         if (Input.GetMouseButtonDown(0))
>         {
>             if (transform.localScale.x > 0)
>             {
>                 padding += 0.05f;
>                 transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y, transform.localScale.z);
>             }
> 
>         }
>     }
> }
> 
> ```



**UGUI**

> UGUI的设计就相对复杂了，首先我们需要添加一个 **Canvas** 画布，接着使用 **Slider** 作为我们的血条主体。通过 **transform** 的设置，使血条恰好在人物 （Ethan）的头上。由于只需要显示一段红色的血条，所以可以将 **Slider** 中的 **Handle Slider Area** 、 **Background** 禁用。
>
> 这时候我们会发现我们的血条非常的小，这是因为我们没有设置其 **MaxValue** 和 **Value** 。我们展开 **Slider** 组件，并设置其 **MaxValue** 为100 ， **Value** 为75。这样，我们的血条就基本设计完毕了。
>
> **但是** ，其实还缺少一步，当我们移动 **Ethan** 的时候，我们会发现血条也跟着转动了。所以这时候我们需要挂载一个脚本，使 **Ethan** 身上的 **Canvas** 永远面向屏幕，这样我们的血条才算真正设计完成。
>
> **具体代码如下：**
>
> ```C#
> using UnityEngine;
> 
> public class LookAtCamera : MonoBehaviour {
> 
> 	void Update () {
> 		this.transform.LookAt (Camera.main.transform.position);
> 	}
> }
> ```



**分析**

> 在我看来 **IMGUI** 是喜欢用代码控制一切的程序员所使用的。它的优点就在于可以通过简单的代码，去控制每一帧发生的变化，并且所需要的代码量也不是很多，它并不会像 **UGUI** 的操作如此繁杂，有那么多的属性需要自己去操控，而且它所控制的东西可以是3D的，而不像 **UGUI** 一样只是一块平面的画布，但是 **IMGUI** 也有它自己的缺点，就像我们这次制作血条一样，我们的需求是血条永远面向屏幕，我一开始是将血条作为人物的子对象，可是血条会跟着人物旋转，不符合要求。无奈只好将血条单独提出来，并设置一个 **transform** 来接收人物的位置，并设置其位置。
>
> **UGUI** 就不会出现上述的问题，他就是一个平面，设置画布永远面向屏幕，问题就解决了。它的优点我认为主要是在布局上，我们使用 **IMGUI** 进行布局的时候，有时候一个 **button** 的位置都要调很久，而在 **UGUI** ，我们只需要拖动就可以随心选择自己想要的位置。除此之外， **UGUI** 还提供了很多其他的以前我在 **IMGUI** 里面没有用到的部件，这些部件的使用会使我们的游戏变得更加丰富，这也随之带来它的缺点，就是操作十分繁琐了。



**效果展示**

https://github.com/yaody7/unity3d-learning/blob/master/HW9/效果展示.mp4



Github：https://github.com/yaody7/unity3d-learning/tree/master/HW9




