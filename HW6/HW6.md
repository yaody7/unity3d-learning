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
> ![1570589583378](https://github.com/yaody7/unity3d-learning/blob/master/HW6/pics/1570589583378.png)
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



	3. **设计 UFO_action 接口（Adapter模式）**

> 由于我们拥有两个 **Action类** ，所以按照 **热拔插** 的设计思想，我们应该设计一个接口来包装这两个 **Action类** 
>
> 接口设计较为简单，我们通过抽象 **UFOFactory** 中两个类共有的方法，即可以抽象出这么一个接口，具体代码如下：
>
> ```c#
> public interface UFO_action
> {
>     void SetSpeed(int speed);
>     void Start();
>     void SetRunning(bool b);
>     GameObject getPlayer();
>     void setPlayer(GameObject g);
>     void Update();
> }
> 
> ```
>
> 接着我们需要在两个Action类里面继承这个接口，并实现这些函数。注意到的是，这里的函数实现是一致的，代码如下：
>
> ```c#
> public void SetSpeed(int speed)
> {
>     this.speed = speed;
> }
> public void SetRunning(bool b)
> {
>     this.running = b;
> }
> public GameObject getPlayer()
> {
>     return this.player;
> }
> public void setPlayer(GameObject g)
> {
>     this.player = g;
> }		
> ```
>
> 接着我们再在 **UFOFactory类** 里面更新函数实现，将从前指定 **action** 编程使用接口来调用函数。步骤较为冗多而且简单，这里就不展示了。





**游戏效果展示**

![1570612241256](https://github.com/yaody7/unity3d-learning/blob/master/HW6/pics/1570612241256.png)

![1570612306178](https://github.com/yaody7/unity3d-learning/blob/master/HW6/pics/1570612306178.png)



游戏视频请见：https://github.com/yaody7/unity3d-learning/blob/master/HW6/movie/Hit_UFO.mp4



### 2、打靶游戏（**可选作业**）

**游戏内容要求：**

> 1. 靶对象为 5 环，按环计分；
> 2. 箭对象，射中后要插在靶上         
>    - **增强要求**：射中后，箭对象产生颤抖效果，到下一次射击 或 1秒以后
> 3. 游戏仅一轮，无限 trials；         
>    - **增强要求**：添加一个风向和强度标志，提高难度





1. **Director 类**

> **Director类** 就是简单的设置场记、以及使用 **singleton模式** 来获得 **Director** 
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
> 
> ```
>
> 



2. **Controller 类**

> 这个类中，我们对资源进行了初始化。而且在 **Update函数** 中设置 **弓**  随着鼠标移动而移动。
>
> ```c#
> void Awake()
> {
>     Random.InitState((int)System.DateTime.Now.Ticks);
>     my_af = new GameObject("Arrow_Factory");
>     my_af.AddComponent<Arrow_Factory>();
>     _director = Director.getInstance();
>     af = Singleton<Arrow_Factory>.Instance;
>     _director.currentController = this;
>     LoadResource();
> }
> void LoadResource()
> {
>     bow = Instantiate(Resources.Load("Prefabs/bow", typeof(GameObject)), new Vector3(0, 0, -4), Quaternion.identity, null) as GameObject;
>     target = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject)), new Vector3(-1, 0, 20), Quaternion.identity, null) as GameObject;
>         
> }
> 
> private void Update()
> {
>     bow.transform.position = Input.mousePosition - new Vector3(374, 138, 0);
> }
> ```



3. **设计My_GUI 类**

> 这里主要做的是显示所用的箭数量，以及当前的成绩。除此之外还要显示风向信息，以及设置了一个重新开始的按钮。
>
> ```c#
> void Start()
> {
>     _director = Director.getInstance();
> }
> 
> private void OnGUI()
> {
>     GUIStyle bb = new GUIStyle();       //创建GUI的格式
>     bb.normal.background = null;
>     bb.normal.textColor = new Color(255, 255, 255);
>     bb.fontSize = 25;
> 
> 
>     string wind = _director.currentController.af.wind.ToString();
>     wind = "风向: " + wind;
>     GUI.Label(new Rect(0.3f * Screen.width, 40, 150, 35), wind, bb);
> 
>     string score = _director.currentController.af.score.ToString();
>     score = "Score: " + score;
>     GUI.Label(new Rect(0.8f * Screen.width, 170, 150, 35), score, bb);
>     string trial = _director.currentController.af.trial.ToString();
>     trial = "Trial: " + trial;
>     GUI.Label(new Rect(0.8f * Screen.width, 200, 150, 35), trial, bb);
>     if (GUI.Button(new Rect(0.75f * Screen.width, 0.7f * Screen.height, 150, 35), "重新开始"))
>     {
>         Application.LoadLevel(0);
>     }
> }
> 
> ```



4. **设计Arrow_Factory 类**

> 这个类充当了箭的工厂以及箭的动作管理器。
>
> 首先我们要初始化好各变量，并为箭加上 **刚体、tremble等组件**
>
> ```c#
> private void Start()
> {
>     wind = new Vector3(Random.Range(100, 300), Random.Range(100, 300), Random.Range(1, 300));
>     director = Director.getInstance();
>     used = new List<GameObject>();
>     not_used = new List<GameObject>();
>     for (int i = 0; i < 5; i++)
>     {
>         GameObject temp = Instantiate(Resources.Load("Prefabs/arrow", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject;
>         temp.AddComponent<Rigidbody>();
>         temp.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 100);
>         temp.GetComponent<Rigidbody>().useGravity = false;
>         temp.GetComponent<tremble>().enabled = false;
>         not_used.Add(temp);
>     }
> }
> ```
>
> 接着在 **FixedUpdate函数** 中，我们设置鼠标按下就射箭的功能。除此之外，在箭射出的时候，要给箭设置上风力。并且我们要检查我们的弓上还有没有箭，若没有就要添加一些箭。
>
> ```c#
> private void FixedUpdate()
> {
>     if (director.currentController.bow != null)
>     {
>         for (int i = 0; i < not_used.Count-1; i++)
>         {
>             not_used[i].transform.position = director.currentController.bow.transform.position + new Vector3(5, 0.1f, 0);
>         }
>         not_used[not_used.Count - 1].transform.position = director.currentController.bow.transform.position + new Vector3(0, 0.1f, 0);
>     }
>     if (Input.GetMouseButtonDown(0)&&not_used.Count>0)
>     {
>         trial++;
>         once = true;
>         used.Add(not_used[not_used.Count - 1]);
>         used[used.Count - 1].GetComponent<Rigidbody>().AddForce(wind);
>         wind = new Vector3(Random.Range(100, 300), Random.Range(100, 300), Random.Range(1, 300));
>         not_used.Remove(not_used[not_used.Count - 1]);
>     }
>     ResetArrow();
> }
> ```
>
> **ResetArrow** 这个函数就是用于给弓补充箭的。
>
> ```c#
> public void ResetArrow()
> {
>     if (not_used.Count == 1)
>     {
>         GameObject temp = Instantiate(Resources.Load("Prefabs/arrow", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject;
>         temp.AddComponent<Rigidbody>();
>         temp.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 100);
>         temp.GetComponent<Rigidbody>().useGravity = false;
>         temp.GetComponent<tremble>().enabled = false;
>         not_used.Add(temp);
>     }
> }
> ```



5. **设计触发器函数**

> 我们新建了一个脚本 **boom** ，当箭射到靶子上的时候，根据触发的触发器数量加分。之后，我们需要将 **tremble脚本** 设置为 **enabled** ，使箭能颤抖0.3秒。
>
> ```c#
> private void OnTriggerEnter(Collider other)
> {
>     if (director != null)
>     {
>         director.currentController.af.used[director.currentController.af.used.Count - 1].GetComponent<Rigidbody>().velocity = Vector3.zero;
>         director.currentController.af.score += 1;
>         if (director.currentController.af.once == true)
>         {
>             director.currentController.af.used[director.currentController.af.used.Count - 1].GetComponent<tremble>().enabled = true;
>             director.currentController.af.once = false;
>         }
>     
> ```



6. **设计tremble类**

> 这个类在 **OnEnable函数** 中获取箭靶上的初始位置，并在 **Update函数** 中，不断快速地变换位置来模拟箭地抖动。同时我们将设置一个 **left_time** 来控制抖动时间。
>
> ```c#
> public class tremble : MonoBehaviour
> {
>     float radian = 0;
>     float per_radian = 3f;
>     Vector3 old_pos;                              
>     public float left_time = 0;              
> 
> 
>     public void OnEnable()
>     {
>         old_pos = transform.position;
>         left_time = 0.3f;
>     }
> 
>     public void Update()
>     {
>         left_time -= Time.deltaTime;
>         if (left_time <= 0)
>         {
>             transform.position = old_pos;
>             Destroy(this.transform.GetComponent<Rigidbody>());
>            this.enabled = false;
>         }
>         if (left_time > 0)
>         {
>             float dy = Random.Range(-0.200f, 0.200f);
>             transform.position = old_pos + new Vector3(0, dy, 0);
>         }
>     }
> }
> ```





**游戏效果展示**

![1570682534677](https://github.com/yaody7/unity3d-learning/blob/master/HW6/pics/1570682534677.png)



游戏视频请见：https://github.com/yaody7/unity3d-learning/blob/master/HW6/movie/arrow.mp4



**Github地址：** https://github.com/yaody7/unity3d-learning/tree/master/HW6