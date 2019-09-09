## HW2

#### 1. 简答题

- 解释游戏对象（GameObjects）和资源（Assets）的区别与联系。

**答：**

1. GameObjects是我们制作游戏时，游戏中运行的主体（主角）。而Assets指的是我们在制作游戏时，可以用于丰富GameObjects的东西。比如我们创建一个Script脚本，并将其挂在Scene中的一个Cube上，那么这个Cube就是游戏主体，而这个用于使Cube有自己的动作的脚本就成为了Assets。

2. 其次，举Unity作为例子。GameObjects的建立是在界面的左上方create，而Assets的建立是在界面的左下方。（其中，Prefabs可以通过拖拽GameObjects到下方Assets框实现，这也是一种创建Assets的过程）

   

- 下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）

**答：**

在*逗分享*上下载几个unity3d的小游戏。

> http://www.idoubi.net/category/unity3d/complete-project

**资源的目录组织结构：**

​										![1567774448491](https://github.com/yaody7/unity3d-learning/blob/master/HW2/pics/1567774448491.png)

​	建立不同的文件夹，对不同的资源进行分类。简单的有Material、Scenes、Scirpt三类。

**游戏对象树的层级结构：**

​										![1567774601876](https://github.com/yaody7/unity3d-learning/blob/master/HW2/pics/1567774601876.png)             

​	子游戏对象存在于父游戏对象之下。比如table有4个子游戏对象。四个chair就会跟着table移动。



- 编写一个代码，使用debug语句来验证MonoBehaviour基本行为或时间触发的条件
  - 基本行为包括Awake() Start() Update() FixedUpdate() LateUpdate()
  - 常用事件包括 OnGUI() OnDisable() OnEnable()
  
  **答：**编写debug语句，举Awake()为例子：
  
  ```c#
  private void Awake()
  {
      Debug.Log("Init Awake");
  }
  ```
  运行结果如下图：
  
  ![1567775238312](https://github.com/yaody7/unity3d-learning/blob/master/HW2/pics/1567775238312.png)
  
  > - Awake：这是在脚本最开始的时候调用的
  > - Start：开始调用Update函数之前调用
  > - Update：在动画的每一帧逗调用一次
  > - FixedUpdate：遇到固定帧的时候调用
  > - LateUpdate：在Update调用的最后调用一次
  > - OnEnable：当对象激活的时候调用一次，在上图中是在Awake后调用
  > - OnDisable：当对象非激活的时候调用一次，再上图中是OnDisable后调用
  > - OnGui：在使用Unity的GUI时进行调用。
  
  
  
- 查找脚本手册，了解 GameObject，Transform，Component 对象
  
  - 分别翻译官方对三个对象的描述（Description）
  
  **答：**
  
  ​	**Game Object：**游戏对象是Unity中的基本对象，它展示了游戏角色，游戏属性，游戏场景。	仅靠他们自己并不能做什么，但他们可以扮演组件的容器，这些容器将实现他们的实体功能。
  
  ​	**Transform：**转换组件决定了每一个场景中对象的位置、旋转以及比例。每一个游戏对象都	有一个转换组件。
  
  ​	**Component：**组件是游戏中对象和行为的小构成部分。他们是每一个游戏对象的功能部分。
  
  
  
  - 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件
  
  ![workwork](https://pmlpml.github.io/unity3d-learning/images/ch02/ch02-homework.png)
  
  **答：**
  
  **Inspector：**这里包含了table的基本属性，比如名称、Tag标记、Layer层次以及Prefab预设
  
  **TransForm：**这里记录table的位置信息：
  
  - Position (0,0,0)  表示位置
  - Rotation (0,0,0) 表示旋转
  - Scale (1,1,1) 表示大小
  
  **Cube：**表示这个table是由Cube做成的
  
  **Box Collider：**碰撞器与触发器，用来对物体之间的行为做出定义
  
  **Mesh Renderer：**网格渲染器，用来设置网格的状态
  
  **Defaul-Material：**这是设置物体的颜色等信息的Component
  
  
  
  - 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）
  
  **答：**
  
  ![1567778960512](https://github.com/yaody7/unity3d-learning/blob/master/HW2/pics/1567778960512.png)



- 整理相关学习资料，编写简单代码验证以下技术的实现：

  - 查找对象

    **答：**`var myfind = GameObject.Find("table");`

  - 添加子对象

    `GameObject son = GameObject.CreatePrimitive(PrimitiveType.Cube);`

    **答：**`son.transform.parent = this.transform;`

  - 遍历对象树

    **答：**`foreach(Transform child in tranform) Debug.Log(child.name);`

  - 清除所有子对象

    **答：**`foreach(Transform child in tranform) Destroy(child.gameObject);`

    

- 资源预设（Prefabs）与 对象克隆 (clone)     
  - 预设（Prefabs）有什么好处？
  
    **答：**预设可以将一个包含许多组件的游戏对象作为资源使用。比如我要手工用砖头搭一坐摩天大厦，我一定是将砖头这个游戏对象打包并直接使用，而不会再重新创建一个cube，再调节参数，再拿来用。这为我们构建游戏对象提供了便利。
  
  - 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
  
    **答：**我们利用预设来更方便地克隆。从某种角度看，预设是将一个游戏对象克隆到了资源库中，当我们需要用的时候，又从资源库中将对象克隆到场景中。
  
  - 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象
  
    **答：**
  
    代码：
  
    > `public class TableBeh : MonoBehaviour`
    > `{`
    >    ` public GameObject table;`
    > `    // Start is called before the first frame update`
    >     `void Start()`
    >     `{`
    >        ` GameObject temp = (GameObject)Instantiate(table, transform.position, transform.rotation);`
    >       `  temp.name = "instance";`
    >     `}`
    > `}`
  
    将预设资源加入：
  
    ![1567780973837](https://github.com/yaody7/unity3d-learning/blob/master/HW2/pics/1567780973837.png)
  
    测试结果：
  
    ![1567781010331](https://github.com/yaody7/unity3d-learning/blob/master/HW2/pics/1567781010331.png)



#### 2. 编程实践，小游戏

- **游戏内容：**井字棋
- **制作大致过程：**

> 1. 使用既有的cube拼成棋盘和棋盘上的线。
> 2. 使用Sphere作为圆，使用两个交叉的cube组成叉，并将其作为预设。
> 3. 编写圆和叉的脚本，并将其挂在到Main Camera上。
> 4. 将所有的Assets分文件夹装好，完成游戏。

- **关键代码解释：**

>```c#
>void Update()
>{
>    mousePosition = Input.mousePosition;
>    float x, z;
>    if (mousePosition.x < 314)
>        x = -3.8f;
>    else if (mousePosition.x < 440)
>        x = 0;
>    else
>        x = 3.8f;
>    if (mousePosition.y > 228)
>        z = 3.8f;
>    else if (mousePosition.y > 123)
>        z = 0;
>    else
>        z = -3.8f;
>    targetPosition = new Vector3(x, 0, z);
>
>//这里是通过测量棋盘中间四个交叉点的值，为圆和叉分配9个格子正中间的位置。
>
>
>
>    //Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distance));
>    targetObject.position = targetPosition;
>
>    if (Input.GetMouseButtonUp(0))
>    {
>        if (judge == 1)
>        {
>            Instantiate(targetObject, targetObject.transform.position, targetObject.transform.rotation);
>            if (x < 0 && z < 0)				//这一段是给标记放上圆或叉的位置
>                x31 = true;
>            else if (x < 0 && z == 0)
>                x21 = true;
>            else if (x < 0 && z > 0)
>                x11 = true;
>            else if (x == 0 && z < 0)
>                x32 = true;
>            else if (x == 0 && z == 0)
>                x22 = true;
>            else if (x == 0 && z > 0)
>                x12 = true;
>            else if (x > 0 && z < 0)
>                x33 = true;
>            else if (x > 0 && z == 0)
>                x23 = true;
>            else
>                x13 = true;
>            if ((x11 && x12 && x13) || (x21 && x22 && x23) || (x31 && x32 && x33) || (x11 && x21 && x31) || (x12 && x22 && x32) || (x13 && x23 && x33) || (x11 && x22 && x33) || (x13 && x22 && x31))			//判断是否满足游戏结束要求
>            {
>                over = true;
>            }
>
>            judge = 0;		//judge用来决定己方回合还是对方回合
>        }
>        else
>            judge = 1;
>    }
>}
>```
>```c#
>private void OnGUI()
>{
>    if (over == true)
>    {
>
>        GUIStyle bb = new GUIStyle();			//创建GUI的格式
>        bb.normal.background = null;
>        bb.normal.textColor = new Color(1, 0, 0);
>        bb.fontSize = 50;
>		//创建GUI Label
>        GUI.Label(new Rect(Screen.width * 0.32f, Screen.height * 0.33f, 300, 300), "Game Over", bb);
>        GUI.Label(new Rect(Screen.width * 0.35f, Screen.height * 0.50f, 300, 300), "White Win", bb);
>        //   GameObject.Find("Main Camera").GetComponent<playball>().enabled = false;
>        //   GameObject.Find("Main Camera").GetComponent<playcha>().enabled = false;
>        if (Input.GetMouseButtonDown(0))		
>            Application.LoadLevel(0);		//游戏结束后，再点击一次重新开始。
>    }
>}
>```



#### 3. 思考题

- 微软 XNA 引擎的 Game 对象屏蔽了游戏循环的细节，并使用一组虚方法让继承者完成它们，我们称这种设计为“模板方法模式”。    
  
  - 为什么是“模板方法”模式而不是“策略模式”呢？
  
  **答：**微软的XNA引擎提供虚方法让继承者完成，就是提供了一个模板来让需要用到这个东西的继承者按照模板来编写自己的代码，这是”模板方法“。至于”策略模式“，指的是所有的方法都是一个”实“方法，而不存在提供虚方法来让别人实现的，”策略模式“应该提供一个可以直接用的方法供以替换。
  
  
  
- 将游戏对象组成树型结构，每个节点都是游戏对象（或数）。     
  - 尝试解释组合模式（Composite Pattern / 一种设计模式）。
  
  **答：**组合模式就是要分类，将一组相似对象作为单一对象，并按照某种规则将这些对象进行分层组合，使其成为一个树形结构。另外，在对象之间要留有接口，方便各个对象进行连接组合。
  
  
  
  - 使用 BroadcastMessage() 方法，向子对象发送消息。你能写出 BroadcastMessage() 的伪代码吗?
  
  **答：**
  
  ​		`foreach(Transform child in tranform) `
  
  ​				`child.test();`
  
  
  
- 一个游戏对象用许多部件描述不同方面的特征。我们设计坦克（Tank）游戏对象不是继承于GameObject对象，而是 GameObject 添加一组行为部件（Component）。     
  - 这是什么设计模式？
  
  **答：**装饰器模式（Decorator）。
  
  
  
  - 为什么不用继承设计特殊的游戏对象？
  
  **答：**用装饰模式可以更加灵活的设计”子类“（并非子类）。若是使用继承，那么就不方便我们随时添加功能。