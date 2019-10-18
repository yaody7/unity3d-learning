# HW7

### 1. 智能巡逻兵

**游戏设计要求：**

> 1. 创建一个地图和若干巡逻兵(使用动画)；
> 2. 每个巡逻兵走一个3~5个边的凸多边型，位置数据是相对地址。即每次确定下一个目标位置，用自己当前位置为原点计算；
> 3. 巡逻兵碰撞到障碍物，则会自动选下一个点为目标；
> 4. 巡逻兵在设定范围内感知到玩家，会自动追击玩家；
> 5. 失去玩家目标后，继续巡逻；



**程序设计要求：**

> 1. 必须使用订阅与发布模式传消息
> 2. 工厂模式生产巡逻兵



**游戏玩法：**

> 地图上会随机产生 **红球** ，**Player** 需要逃避巡逻兵追捕并拾起 **红球** ，拾起一个计一分，被巡逻兵抓到则游戏结束。



1. **设计Player预设**

> 从 **Asset Store** 下载的资源：
>
> ![1571405512825](C:\Users\89481\AppData\Roaming\Typora\typora-user-images\1571405512825.png)
>
> 该资源中含有 **Run**、**Die** 的动作。我们将利用其来做设计我们的脚本。
>
> **Player脚本：**
>
> 我们通过W、A、S、D移动 **Player** ，**Player** 作为一个 **Publisher** 发布其所在区域位置，供 **Subscriber** 接收。所以我们也需要设计一个 **judge_sign** 函数来确定 **Player** 的位置，若是变换区域，就要发布公告。
>
> **具体代码如下：**
>
> ```c#
> public class Player : MonoBehaviour
> {
>     public delegate void AnimationHandler();
>     Animation animation;
> //    public static Player instance;
>     public AnimationClip Die;
>     public AnimationClip Run;
>     public AnimationClip Idle;
>     public AnimationHandler animationHandler;
> 
> 
>     public delegate void ChangeHandler();
>     public ChangeHandler changeHandler;
> 
>     public int sign;    // 1, 2, 3, 4
>     public void judge_sign()
>     {
>         int tempsign;
>         if (transform.position.x < 0)
>         {
>             if (transform.position.z > 0)
>                 tempsign = 1;
>             else
>                 tempsign = 3;
>         }
>         else
>         {
>             if (transform.position.z > 0)
>                 tempsign = 2;
>             else
>                 tempsign = 4;
>         }
>         if (tempsign != sign)
>         {
>             sign = tempsign;
>             changeHandler();
>         }
>         Debug.Log(sign);
>     }
>     public void Start()
>     {
>         animationHandler = PlayRun;
>         sign = 3;
>         changeHandler = Director.getInstance().currentController._NPCfactory.NPC1.GetComponent<NPC>().Observe;
>         changeHandler += Director.getInstance().currentController._NPCfactory.NPC2.GetComponent<NPC>().Observe;
>         changeHandler += Director.getInstance().currentController._NPCfactory.NPC3.GetComponent<NPC>().Observe;
>    //     instance = this;
>         animation = GetComponent<Animation>();
> 
>     }
>     public void PlayIdle()
>     {
>         animation.Play(Idle.name);
>     }
>     public void PlayDie()
>     {
>         animation.Play(Die.name);
>     }
> 
>     public void PlayRun()
>     {
>         animation.Play(Run.name);
>     }
>     void Update()
>     {
>         if (animationHandler != PlayDie)
>         {
>             float KeyVertical = Input.GetAxis("Vertical");
>             float KeyHorizontal = Input.GetAxis("Horizontal");
>             Vector3 newDir = new Vector3(KeyHorizontal, 0, KeyVertical).normalized;
>             if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
>             {
>                 transform.forward = Vector3.Lerp(transform.forward, newDir, 1000000);
>             }
>             transform.position += newDir * Time.deltaTime * 3;
>         }
>         animationHandler();
>         judge_sign();
>         if (animationHandler == PlayDie)
>             this.enabled = false;
>     }
> 
> }
> ```





2. **设计NPC预设**

> 下载了和 **Player** 配套的资源：
>
> ![1571406138442](C:\Users\89481\AppData\Roaming\Typora\typora-user-images\1571406138442.png)
>
> 里面有 **Walk、Run、Attack** 的动画，我们可以用此设计我们的脚本。
>
> **NPC脚本：**
>
> **NPC** 相对于 **Player** 而言复杂一些。 **NPC** 的移动是这样实现的：按照朝向向前 **走动** ，撞到障碍物时旋转朝向，并接着按朝向向前 **走动** ；而当 **Player** 进入 **NPC** 所在区域时， **NPC** 将往 **Player** 位置 **跑动** ，当 **Player** 转移到另外区域后，重新恢复初始巡逻状态。由此可见， **NPC** 是 **Subscriber** ，接收 **Player** 的公告，并依次改变自己的状态。
>
> **具体代码如下**：
>
> ```c#
> public class NPC : MonoBehaviour
> {
>     public int sign;   //1,2,3,4
>     Vector3 current_position;
>     public delegate void AnimationHandler();
>     Animation animation;
>     public static NPC instance;
>     public AnimationClip Attack;
>     public AnimationClip Run;
>     public AnimationClip Walk;
>     public AnimationHandler animationHandler;
>     void Start()
>     {
>         current_position = transform.position;
>         instance = this;
>         animationHandler = PlayWalk;
>         animation = GetComponent<Animation>();
> 
>     }   
>     public void Observe()
>     {
>         if (Director.getInstance().currentController._player.GetComponent<Player>().sign == this.sign)
>         {
>             animationHandler = PlayCatch;
>         }
>         else
>         {
>             animationHandler = PlayWalk;
>         }
>     }
>     public void PlayCatch()
>     {
>         transform.forward = (Director.getInstance().currentController._player.transform.position - this.transform.position).normalized;
>         transform.position = Vector3.MoveTowards(transform.position, Director.getInstance().currentController._player.transform.position, 3 * Time.deltaTime);
>         animation.Play(Run.name);
>     }
>     public void PlayWalk()
>     { 
>         animation.Play(Walk.name);
>         if ((transform.position - current_position).sqrMagnitude < 0.0001)
>             this.transform.RotateAround(transform.position, Vector3.up, Random.Range(-180, 180));
>         current_position = transform.position;
>         transform.position += 1.5f * Time.deltaTime * transform.forward;
>     }
> 
>     public void PlayAttack()
>     {
>         animation.Play(Attack.name);
>     }
>     void Update()
>     {
>         animationHandler();
>     }
> 
>     private void OnCollisionEnter(Collision collision)
>     {
>         if (animationHandler != PlayAttack)
>         {
>             this.transform.RotateAround(transform.position, Vector3.up, Random.Range(-180, 180));
>         }
>         if (collision.collider.gameObject == Director.getInstance().currentController._player)
>         {
>             animationHandler = PlayAttack;
>             transform.forward = (Director.getInstance().currentController._player.transform.position-this.transform.position).normalized;
>             Director.getInstance().currentController._player.GetComponent<Player>().animationHandler = Director.getInstance().currentController._player.GetComponent<Player>().PlayDie;
>         }
>         animationHandler();
>     }
> }
> ```



3. **设计Controller**

> **Controller** 的设计也比较简单，它负责统筹各个组件之间的关系，所以在其中我们需要设置一下几个成员变量：**_NPCfactory、__director、my_NPCfactory、_player、_playground、_bonus** ，并对其进行初始化、实例化。需要注意的是， **NPCfactory** 采用的是单例模式。
>
> **具体代码如下**：
>
> ```c#
> public class Controller : MonoBehaviour
> {
>     public NPCFactory _NPCfactory;
>     public Director _director;
>     private GameObject my_NPCfactory;
>     public GameObject _player;
>     public GameObject _playground;
>     public GameObject _bonus;
>     private void Awake()
>     {
>         Random.InitState((int)System.DateTime.Now.Ticks);
>         _director = Director.getInstance();
>         _director.currentController = this;
>         _player = Object.Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject)), new Vector3(-2, 0, -2), Quaternion.identity, null) as GameObject;
>         my_NPCfactory = new GameObject("NPC_Factory");
>         my_NPCfactory.AddComponent<NPCFactory>();
>         _NPCfactory = Singleton<NPCFactory>.Instance;
>         _playground = Object.Instantiate(Resources.Load("Prefabs/Playground", typeof(GameObject)), new Vector3(5.286964f, -1.301903f, 0.5366425f), Quaternion.identity, null) as GameObject;
>         _bonus = Object.Instantiate(Resources.Load("Prefabs/Bonus", typeof(GameObject)), new Vector3(-4, 0.5f, -4), Quaternion.identity, null) as GameObject;
>     }
> 
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



5. **设计NPCFactory**

> 本次 **工厂模式** 运用的不好，本来应该涉及 **NPC** 的回收利用的。但是这次偷懒，因为游戏没有设计轮次，那就顺便不设计重新开始按钮了，故 **NPCFactory** 只剩下一个制造 **NPC** 的功能了。
>
> **具体代码如下**：
>
>  ```c#
>       public class NPCFactory : MonoBehaviour
>    {
>       public int choose = 0;  //0 for Kinematics_actions   1 for Dynamic_actions
>     public List<GameObject> used;
>       public List<GameObject> not_used;
>        public int score = 0;
>       public GameObject NPC1;
>     public GameObject NPC2;
>       public GameObject NPC3;
>        static int once = 1;
>       private void Awake()
>     {
>        if (once == 1)
>         {
>            NPC1 = Object.Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject)), new Vector3(-2, 0, 2), Quaternion.identity, null) as GameObject;
>                NPC1.GetComponent<NPC>().sign = 1;
>            NPC2 = Object.Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject)), new Vector3(2, 0, 2), Quaternion.identity, null) as GameObject;
>                NPC2.GetComponent<NPC>().sign = 2;
>                NPC3 = Object.Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject)), new Vector3(2, 0, -2), Quaternion.identity, null) as GameObject;
>                NPC3.GetComponent<NPC>().sign = 4;
>                once++;
>            }
>        }
>      
>    }
>     ```
>    



6. **设计bonus机制**

> 我们的 **bonus** 就是一颗红球： **Player** 需要不断地拾取这颗红球获得加分。而由于在设计初期，我们红球的重新产生方式是完全随机的，导致有时候在同个区域能多次拾取红球。所以进行了改进：红球的位置会按照：1->2->3->4进行变换，而各区域里的位置是随机生成的。并且在红球上还安装了一个碰撞器，被 **Player** 碰到后就会变换位置，并且进行加分操作。
>
> **具体代码如下**：
>
> ```c#
> public class bonus : MonoBehaviour
> {
>     public int score;
>     int sign;    //1,2,3,4
>     // Start is called before the first frame update
>     void Start()
>     {
>         score = 0;
>         sign = 3;
>     }
> 
>     // Update is called once per frame
>     void Update()
>     {
>         
>     }
>     private void OnCollisionEnter(Collision collision)
>     {
>         if (collision.collider.gameObject == Director.getInstance().currentController._player) {
>             score++;
>             if (sign == 1)
>             {
>                 this.transform.position = new Vector3(Random.Range(0.5f, 6.5f), 0.5f, Random.Range(1.5f, 6.5f));
>                 sign = 2;
>             }
>             else if (sign == 2)
>             {
>                 this.transform.position = new Vector3(Random.Range(-6.5f, -0.5f), 0.5f, Random.Range(-6.5f, 0.5f));
>                 sign = 3;
>             }
>             else if (sign == 3)
>             {
>                 this.transform.position = new Vector3(Random.Range(0.5f, 6.5f), 0.5f, Random.Range(-6.5f, -0.5f));
>                 sign = 4;
>             }
>             else if (sign == 4)
>             {
>                 this.transform.position = new Vector3(Random.Range(-6.5f, -0.5f), 0.5f, Random.Range(1.5f, 6.5f));
>                 sign = 1;
>             } 
>         }
>     }
> }
> 
> ```



7. **设计My_GUI**

> 该类的主要作用就是创建 **Score** 的label。并且当 **Player** 被抓住之后显示最终得分。
>
> **具体代码如下**：
>
> ```c#
> public class My_GUI : MonoBehaviour
> {
>     public Director _director;
>     // Start is called before the first frame update
>     void Start()
>     {
>         _director = Director.getInstance();
>     }
> 
>     private void OnGUI()
>     {
>         if (_director.currentController._player.GetComponent<Player>().animationHandler == _director.currentController._player.GetComponent<Player>().PlayDie)
>         {
>             GUIStyle bb = new GUIStyle();   //创建GUI的格式
>             bb.normal.background = null;
>             bb.normal.textColor = new Color(0, 0, 0);
>             bb.fontSize = 50;
>             string score = _director.currentController._bonus.GetComponent<bonus>().score.ToString();
>             score = "FinalScore: " + score;
>             GUI.Label(new Rect(0.3f * Screen.width, 0.4f*Screen.height, 150, 35), score, bb);
>         }
>         else
>         {
> 
>             GUIStyle bb = new GUIStyle();   //创建GUI的格式
>             bb.normal.background = null;
>             bb.normal.textColor = new Color(0, 0, 0);
>             bb.fontSize = 25;
>             string score = _director.currentController._bonus.GetComponent<bonus>().score.ToString();
>             score = "Score: " + score;
>             GUI.Label(new Rect(0.7f * Screen.width, 270, 150, 35), score, bb);
>             
>         }
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

![1571406871879](C:\Users\89481\AppData\Roaming\Typora\typora-user-images\1571406871879.png)



![1571406918814](C:\Users\89481\AppData\Roaming\Typora\typora-user-images\1571406918814.png)



游戏视频请见：-----------




