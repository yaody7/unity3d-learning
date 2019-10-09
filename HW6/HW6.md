# HW6

### 1. 改进飞碟（Hit UfO）游戏：

**游戏内容要求：**

> 1. 按adapter模式设计图修改飞碟游戏
> 2. 使它同时支持物理运动与运动学（变换）运动



**游戏设计：**

> 使用adapter模式更新游戏，使其同时支持两种游戏运动。
>
> 下面将对更新部分做介绍。



1. **更改原 UFO_action 类 为 UFO_Kinematics_action 类**

> 由于我们将创建一个新的动力学action，所以我们将原来的类改为 **运动学action** 来区分两种action。需要做出的更改十分简单，如下面所示：
>
> ![1570589583378](C:\Users\89481\AppData\Roaming\Typora\typora-user-images\1570589583378.png)
>
> 注意到的是，当我们修改脚本名称时，要对应修改其类的名称，否则会报错。
>
> ```c#
> public class UFO_Kinematics_action : ScriptableObject
> ```
>
> 



2. **设计UFO_Dinamics_action 类**

> **UFO_Dinamics_action类** 的设计可以仿照 **UFO_Kinematics类** 的设计。但是需要注意到的是，我们这里在 **Start函数** 中为飞碟添加了 **刚体组件** ，并且为其设置了初速度。所以在这个action被创建的时候，飞碟就开始飞了，一定要注意到这点，否则会影响后面的设计。具体代码如下：
>
> ```c#
> public void Start()
> {
>     _director = Director.getInstance();
>     if (player.GetComponent<Rigidbody>() == null)
>         player.AddComponent<Rigidbody>();
>     start = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
>     if (start.x < 10 && start.x > -10)
>         start.x *= 4;
>     if (start.y < 10 && start.y > -10)
>         start.y *= 4;
>     end = new Vector3(0, 0, 20);
>     player.transform.position = start;
>     setColor();
>     Rigidbody rigit = player.GetComponent<Rigidbody>();
>     rigit.velocity = (end - start) * speed * Random.Range(0.0f, 0.10f);
>     rigit.useGravity = false;
> }
> ```
>
>  
>
> 我们接着就要对应设计其 **update函数** ，需要注意到的是，我们在 **start函数** 中已经为飞碟设置了初速度，所以在就不用像 **Kinematics_action** 那样每一帧都更新飞碟位置了。我们要做的是检查飞碟是否飞出屏幕外。然而因为飞碟的位置是随机的，又由于他们是刚体，很多时候会产生碰撞而做出一些奇怪的运动轨迹。所以在 **update函数** 中，我记录了其调用次书。若是调用次数 **超过300** ，则该飞碟推入 **not_hit** 函数。具体代码如下：
>
> ```c#
> public void Update()
> {
>     framecount++;
>     if(framecount>300)
>         this._director.currentController._UFOfactory.not_hit(this.player);
> 
>     Rigidbody rigit = player.GetComponent<Rigidbody>();
>     if (running == false)
>     {
>         rigit.velocity = Vector3.zero;
>         framecount = 0;
>     }
>     if (player.transform.position.x < -100 || player.transform.position.x > 100 || player.transform.position.x < -100 || player.transform.position.x > 100 || player.transform.position.x < -100 || player.transform.position.x > 100)
>     {
>         rigit.velocity = Vector3.zero;
>         this._director.currentController._UFOfactory.not_hit(this.player);
>     }
> }
> ```
>
>  
>
> 后面的 **setColor函数** 与前面的设计相同，在这里就不赘述了。



3. **更新 UFOFactory 类**

> 由于 **Kinematics_action** 需要在 **update函数** 中调用，而 **Dynamics_action** 需要在 **fixed_update函数** 中调用，所以我们设置标记值，
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