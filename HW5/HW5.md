# HW5

### 1. 编写一个简单的鼠标打飞碟（Hit UFO）游戏

**游戏内容要求：**

> 1. 游戏有 n 个 round，每个 round 都包括10 次 trial；
> 2. 每个 trial 的飞碟的色彩、大小、发射位置、速度、角度、同时出现的个数都可能不同。它们由该 round 的 ruler 控制；
> 3. 每个 trial 的飞碟有随机性，总体难度随 round 上升；
> 4. 鼠标点中得分，得分规则按色彩、大小、速度不同计算，规则可自由设定。



**游戏的要求：**

> 1. 使用带缓存的工厂模式管理不同飞碟的生产与回收，该工厂必须是场景单实例的！具体实现见参考资源 Singleton 模板类
> 2. 近可能使用前面 MVC 结构实现人机交互与游戏模型分离



**游戏设计：**

> 本次设计使用了单例模式和MVC模式，下面将按照实际设计的顺序来展示本次 **Hit UFO** 游戏的设计。



1. **设计飞碟预设**

> 由于U3D没有给我们提供飞碟的形状，所以我们只好自己设计。
>
> 我选择的方式是将一个圆球外面再套上一个圆盘，这样就做成了我们小时候印象中最经典飞碟的样子。由于我们需要给飞碟添上颜色，所以还需要给飞碟添上material组件。这就是我们的飞碟预设了。
>
> ![1569544580456](https://github.com/yaody7/unity3d-learning/blob/master/HW5/pics/1569544580456.png)
>
> ![1569544620194](https://github.com/yaody7/unity3d-learning/blob/master/HW5/pics/1569544620194.png)



2. **设计Director**

> **Director** 的设计十分简单，使用单例模式。设置一个 **Director类** 的private static成员变量，并设置一个与之配套的 **getInstance()** 的成员函数。再设置一个 **currentController** 的成员变量就可以了。
>
> ```c#
> public class Director : System.Object
> {
>     private static Director _instance;
>     public Controller currentController { get; set; }
>     public static Director getInstance()
>     {
>         if (_instance == null)
>         {
>             _instance = new Director();
>         }
>         return _instance;
>     }
> }
> ```



3. **设计Controller**

> **Controller** 的设计也比较简单，它负责统筹各个组件之间的关系，所以在其中我们需要设置一下几个成员变量：**_UFOfactory、__director、my_diskfactory** ，并对其进行初始化、实例化。需要注意的是， **_UFOfactory** 采用的是单例模式。
>
> ```c#
> public class Controller : MonoBehaviour
> {
>     public UFOFactory _UFOfactory;
>     public Director _director;
>     private GameObject my_diskfactory;
>     void Awake()
>     {
>         Random.InitState((int)System.DateTime.Now.Ticks);
>         my_diskfactory = new GameObject("Disk_Factory");
>         my_diskfactory.AddComponent<UFOFactory>();
>         _director = Director.getInstance();
>         _UFOfactory = Singleton<UFOFactory>.Instance;
>         _director.currentController = this;
>     }
> }
> 
> ```



4. **设计UFOaction**

> **UFOaction** 定义了UFO的飞行动作，以下是其详细介绍。
>
> - **成员变量：**
>
>   - `public GameObject player`
>
>     记录该动作所归属的对象
>
>   - `Vector3 start`
>
>     记录UFO飞行的初始位置
>
>   - `Vector3 end`
>
>     记录UFO飞行的结束位置
>
>   - `int speed`
>
>     记录UFO飞行的速度
>
>   - `bool running`
>
>     运行态标志位
>
> - **成员函数**
>
>   - **Start()**
>
>     设置UFO **开始位置** 以及 **结束位置** ，并调用 **setColor函数** 调整其颜色。
>
>     ```c#
>         public void Start()
>         {
>             _director = Director.getInstance();
>             start = new Vector3(Random.Range(-20f,20f), Random.Range(-20f, 20f), 0);
>             if (start.x < 10 && start.x > -10)
>                 start.x *= 4;
>             if (start.y < 10 && start.y > -10)
>                 start.y *= 4;
>             end = new Vector3(-start.x, -start.y, 0);
>             player.transform.position = start;
>             setColor();
>         }
>     ```
>
>     
>
>   - **Update()**
>
>     使用 **MoveTowards函数** 移动UFO。并且判定UFO是否移动到其结束位置，若是移动到其结束位置，证明其未被集中，则调用 **not_hit** 函数，回收UFO。
>
>     ```c#
>         public void Update()
>         {
>             if (running)
>             {
>                 player.transform.position = Vector3.MoveTowards(player.transform.position, end, speed * Time.deltaTime);
>                 if (player.transform.position == end)
>                 {
>                     this._director.currentController._UFOfactory.not_hit(this.player);   
>                 }
>             }
>         }
>     ```
>
>     
>
>   - **setColor()**
>
>     使用 **Random** 随机一个数，并根据这个数设置UFO的颜色。
>
>     ```c#
>     public void setColor()
>     {
>         int color = Random.Range(1, 4);
>         switch (color)
>         {
>             case 1:
>                 player.GetComponent<MeshRenderer>().material.color = Color.red;
>                 foreach (Transform child in player.transform)
>                 {
>                     child.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
>                 }
>                 break;
>             case 2:
>                 player.GetComponent<MeshRenderer>().material.color = Color.yellow;
>                 foreach (Transform child in player.transform)
>                 {
>                     child.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
>                 }
>                 break;
>             case 3:
>                 player.GetComponent<MeshRenderer>().material.color = Color.blue;
>                 foreach (Transform child in player.transform)
>                 {
>                     child.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
>                 }
>                 break;
>             default:
>                 break;
>         }
>     }
>     ```



5. **设计UFOfactory**

> **UFOfactory** 的设计就比较复杂了，因为UFO的动作比较简单，所以我将UFO的动作管理员也整合到了 **UFOfactory** 中。
>
> - **成员变量：**
>
>   - `public List<GameObject> used`	
>   
>      存储已经放飞的UFO	
>   
>     - `public List<Gameobejct> not_used`
>   
>        存储飞回来了的UFO
>   
>     - `public List<UFO_action> actions`
>   
>        存储UFO动作，小型动作管理器
>   
> - **成员函数：**
>
>   - **Start()**
>
>      为各个List指针创建List对象，并为各个List添加其对应成员。并为各个 **UfO_action** 绑定对应的 **Player** 。
>
>     ```c#
>          private void Start()
>          {
>              used = new List<GameObject>();
>              not_used = new List<GameObject>();
>              actions = new List<UFO_action>();
>              for(int i = 0; i < 10; i++)
>              {
>                  not_used.Add(Object.Instantiate(Resources.Load("Prefabs/UFO", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject);
>                  actions.Add(ScriptableObject.CreateInstance<UFO_action>());
>              }
>              for(int i = 0; i < 10; i++)
>              {
>                  actions[i].player = not_used[i];
>              }
>          }
>     ```
>
>     
>
>    
>
>   - **Update()**
>
>      调用各个 **处于工作状态的UFO_action** 的 **Update函数** ，在 **round < 10** 的时候调用 **get_ready函数** 来更新轮次。并在 **round == 11** 的时候停止对 **UFO_action** 的操作。
>
>     ```c#
>         private void Update()
>         {
>             if (round <= 10)
>             {
>                 for (int i = 0; i < 10; i++)
>                 {
>                     actions[i].Update();
>                 }
>                 if (not_used.Count == 10)
>                 {
>                     round += 1;
>                     if (round <= 10)
>                         get_ready(round);
>                 }
>             }     
>         }
>     ```
>
>   
>
>     - **hitted(GameObject g)**
>
>       判别被击中的UFO是什么色的，并对应给分。将击中的UFO从 **used列表** 移动到 **not_used列表** ，并调整其为初始位置，再将其对应的 **UFO_action** 调为非running。
>
>       ```c#
>           public void hitted(GameObject g)
>           {
>               if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.red)
>                   score += 3;
>               else if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.yellow)
>                   score += 2;
>               else if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.blue)
>                   score += 1;
>               this.used.Remove(g);
>               g.transform.position = new Vector3(0, -20, 0);
>               for(int i = 0; i < 10; i++)
>               {
>                   if (actions[i].player == g)
>                       actions[i].running = false;
>               }
>               this.not_used.Add(g);
>           }
>       ```
>
>    ​    
>
>     - **not_hit(GameObject g)**
>
>       **not_hit函数** 与 **hitted函数** 只不过是减去了加分的环节。
>
>       ```c#
>           public void not_hit(GameObject g)
>           {
>               this.used.Remove(g);
>               g.transform.position = new Vector3(0, -20, 0);
>               for (int i = 0; i < 10; i++)
>               {
>                   if (actions[i].player == g)
>                       actions[i].running = false;
>               }
>               this.not_used.Add(g);
>           }
>       ```
>
>       
>
>     - **get_ready(int round)**
>
>       这个函数是用于做UFO起飞之前的准备工作，它将 **not_used列表** 中所有的UFO移入 **used列表** 并按轮次调整其飞行速度，并调用 **UFO_action** 的 **Start函数** 做初始化，并将其设为 **running态** 。
>
>       ```c#
>           public void get_ready(int round)
>           {
>               for(int i = 0; i < 10; i++)
>               {
>                   used.Add(not_used[0]);
>                   not_used.Remove(not_used[0]);
>                   actions[i].speed = round + 2;
>                   actions[i].Start();
>                   actions[i].running = true;
>               }
>           }
>       ```



6. **设计Hit_UFO**

> 这个类的设计比较简单，主要就是使用 **Ray** 来定位鼠标的位置以及鼠标点击到的游戏物体，当点击到物体的时候调用 **UFOfactory** 的 **hitted函数** 即可。
>
> ```c#
> public class Hit_UFO : MonoBehaviour
> {
> 
>     public GameObject cam;
>     public Director director;
>     private void Start()
>     {
>         director = Director.getInstance(); 
>     }
>     // Update is called once per frame
>     void Update()
>     {
>         if (Input.GetButtonDown("Fire1"))
>         {
> 
>             Vector3 mp = Input.mousePosition; //get Screen Position
> 
>             //create ray, origin is camera, and direction to mousepoint
>             Camera ca;
>             if (cam != null) ca = cam.GetComponent<Camera>();
>             else ca = Camera.main;
> 
>             Ray ray = ca.ScreenPointToRay(Input.mousePosition);
> 
>             //Return the ray's hit
>             RaycastHit hit;
>             if (Physics.Raycast(ray, out hit))
>             {
>                 director.currentController._UFOfactory.hitted(hit.transform.gameObject);
>             }
>         }
>     }
> }
> ```



7. **设计My_GUI**

> 该类的主要作用就是创建 **Score** 和 **Round** 的label。并在轮次到达11的时候，停止显示 **Score** 和 **Round** ，而在屏幕中间显示一个大的 **Final Score** 。
>
> ```c#
> private void OnGUI()
> {
>     int my_round = _director.currentController._UFOfactory.round;
>     if (my_round == 11)
>     {
>         GUIStyle ending = new GUIStyle();
>         ending.normal.background = null;
>         ending.normal.background = null;
>         ending.normal.textColor = new Color(255, 255, 255);
>         ending.fontSize = 80;
>         string ending_score = "Final Score: " + _director.currentController._UFOfactory.score.ToString();
>         GUI.Label(new Rect(0.13f * Screen.width, 0.4f * Screen.height, 300, 300), ending_score, ending);
>     }
>     else
>     {
>         string round = my_round.ToString();
>         round = "Round: " + round;
>         GUIStyle bb = new GUIStyle();       //创建GUI的格式
>         bb.normal.background = null;
>         bb.normal.textColor = new Color(255, 255, 255);
>         bb.fontSize = 25;
>         GUI.Label(new Rect(0.8f * Screen.width, 240, 150, 35), round, bb);
>         string score = _director.currentController._UFOfactory.score.ToString();
>         score = "Score:" + score;
>         GUI.Label(new Rect(0.8f * Screen.width, 270, 150, 35), score, bb);
>     }
> }
> ```



8. **设计Singleton**

> 差点忘了单例模式的模板，这是按照老师给出的资料设计的，使用到了 **FindObjectOfType** 来寻找一个类型的单例，并返回这个单例。
>
> ```c#
> public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
> {
>     protected static T instance;
>     public static T Instance
>     {
>         get
>         {
>             if (instance == null)
>             {
>                 instance = (T)FindObjectOfType(typeof(T));
>                 if (instance == null)
>                 {
>                     Debug.LogError("No instance of " + typeof(T));
>                 }
>             }
>             return instance;
>         }
>     }
> }
> 
> ```



**游戏效果展示**

![1569555493990](https://github.com/yaody7/unity3d-learning/blob/master/HW5/pics/1569555493990.png)



游戏视频请见：https://github.com/yaody7/unity3d-learning/blob/master/HW5/movie/Hit_UFO.mp4



### 2、编写一个简单的自定义 Component （**选做**）

  **用自定义组件定义几种飞碟，做成预制**     

> 参考官方脚本手册 https://docs.unity3d.com/ScriptReference/Editor.html
>
> 实现自定义组件，编辑并赋予飞碟一些属性

**答：**

我们可以编写旋转脚本组件，参照我们之前做过的太阳系的小游戏，可以让UFO使用不同的转动轴进行转动。

```c#
public class around : MonoBehaviour
{
    public float speed;
    public float yangle, zangle;
    void Update()
    {
        Vector3 axis = new Vector3(0, yangle, zangle);
        this.transform.RotateAround(new Vector3(0, 0, 0), axis, speed * Time.deltaTime);
        this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }
}
```

通过，使用不同的速度与轴方向，就可以让我们的UFO按照不同的旋转轴旋转。将设计好的不同的组件挂载到不同的UFO上，就做好了几种飞碟的预制。