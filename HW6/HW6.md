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
> public interface UFO_action
> {
>  void SetSpeed(int speed);
>  void Start();
>  void SetRunning(bool b);
>  GameObject getPlayer();
>  void setPlayer(GameObject g);
>  void Update();
> }
> 
> ```
>
> 接着我们需要在两个Action类里面继承这个接口，并实现这些函数。注意到的是，这里的函数实现是一致的，代码如下：
>
> ```c#
> public void SetSpeed(int speed)
> {
>  this.speed = speed;
> }
> public void SetRunning(bool b)
> {
>  this.running = b;
> }
> public GameObject getPlayer()
> {
>  return this.player;
> }
> public void setPlayer(GameObject g)
> {
>  this.player = g;
> }		
> ```
>
> 接着我们再在 **UFOFactory类** 里面更新函数实现，将从前指定 **action** 编程使用接口来调用函数。步骤较为冗多而且简单，这里就不展示了。





**游戏效果展示**

![1570612241256](https://github.com/yaody7/unity3d-learning/blob/master/HW6/pics/1570612241256.png)

![1570612306178](https://github.com/yaody7/unity3d-learning/blob/master/HW6/pics/1570612306178.png)



游戏视频请见：https://github.com/yaody7/unity3d-learning/blob/master/HW6/movie/Hit_UFO.mp4