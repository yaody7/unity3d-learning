# HW8

### 1. 完善汽车尾气

**设计要求：**

> 1. 使用官方资源资源 Vehicle 的 car， 使用 Smoke 粒子系统模拟启动发动、运行、故障等场景效果



**使用方法：**

> 设置了4个按钮：
>
> - **爆炸：** 小车爆炸，产生爆炸效果
> - **启动：** 小车启动，产生白烟
> - **故障：** 小车发生故障，产生灰烟，需要在启动的情况下使用
> - **正常行驶：** 小车正常行驶，产生白烟，需要在启动的情况下使用



1. **设计白烟粒子系统**

> 在小车上加入一个 **empty** 对象，并挂载 **Particle System** 。
>
> **Particle System设置如下：**
>
> - **选用部件：**
>
>   ![1571733416831](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733416831.png)
>
> - **总控：**
>
>    ![1571733464840](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733464840.png)
>
> - **Emission：**
> 
>   ![1571733509840](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733509840.png)
>    
>    - **Shape：**
> 
>      ![1571733549342](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733549342.png)
>    
>    - **Renderer：**
>    
>   ![1571733589460](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733589460.png)
> 
>    - **粒子：**
>    
>   ![1571733610458](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733610458.png)
>    
>    **效果展示：**
>    
>    ![1571733666448](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733666448.png)



2. **设计灰烟粒子系统**

> 灰烟粒子系统与白烟粒子系统大致相同，只是将粒子换成了灰色的粒子，这里不再赘述。
>
> ![1571406138442](https://github.com/yaody7/unity3d-learning/blob/master/HW7/pics/1571406138442.png)
>
> 里面有 **Walk、Run、Attack** 的动画，我们可以用此设计我们的脚本。
>
> **NPC脚本：**
>
> **NPC** 相对于 **Player** 而言复杂一些。 **NPC** 的移动是这样实现的：按照朝向向前 **走动** ，撞到障碍物时旋转朝向，并接着按朝向向前 **走动** ；而当 **Player** 进入 **NPC** 所在区域时， **NPC** 将往 **Player** 位置 **跑动** ，当 **Player** 转移到另外区域后，重新恢复初始巡逻状态。由此可见， **NPC** 是 **Subscriber** ，接收 **Player** 的公告，并依次改变自己的状态。
>
> **具体代码如下**：
>
> 



3. **设计爆炸效果**

> 设置一个 **empty** 对象，并挂载 **Particle System** 。
>
> **Particle System设置如下：**
>
> - **选用部件：**
>
>   ![1571733891138](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733891138.png)
>
> - **总控：**
>
>    ![1571733907300](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733907300.png)
>
> - **Emission：**
>
>   ![1571733931836](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733931836.png)
>
> - **Color over Lifetime**：
>
>   ![1571733976255](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571733976255.png)
>
> - **Size over Lifetime：**
>
>   ![1571734148601](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571734148601.png)
>
> - **Texture Sheet Animation：**
>
>   ![1571734341968](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571734341968.png)
>
> - **Renderer**：
>
>   ![1571734369467](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571734369467.png)
>
> - **粒子：**
>
>   ![1571734383606](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571734383606.png)
>
> **效果展示：**
>
> ![1571734495999](https://github.com/yaody7/unity3d-learning/blob/master/HW8/pics/1571734495999.png)



4. **设计MyGUI**

> MyGUI设置了4个按钮，并对按钮的点击事件做一些简单的处理。因为处理过分简单，就直接卸载MyGUI里面了。
>
> **具体代码如下**：
>
> ```c#
> public class MyGUI : MonoBehaviour
> {
>     // Start is called before the first frame update
>     GameObject mycar;
>     GameObject boom;
>     bool start = false;
>     void Start()
>     {
>         mycar = Object.Instantiate(Resources.Load("Prefabs/SkyCar", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity, null) as GameObject;
>         boom = Object.Instantiate(Resources.Load("Prefabs/Boom", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity, null) as GameObject;
>         mycar.transform.forward = new Vector3(90, 0, 0);
>     }
> 
>     // Update is called once per frame
>     void Update()
>     {
>         
>     }
>     private void OnGUI()
>     {
>         if (GUI.Button(new Rect(0.7f * Screen.width, 250, 150, 35), "启动"))
>         {
>             mycar.transform.Find("normal").gameObject.GetComponent<ParticleSystem>().Play();
>             start = true;
>         }
>         if (GUI.Button(new Rect(0.7f * Screen.width, 290, 150, 35), "故障")&&start)
>         {
>             mycar.transform.Find("normal").gameObject.GetComponent<ParticleSystem>().Stop();
>             mycar.transform.Find("broken").gameObject.GetComponent<ParticleSystem>().Play();
>         }
>         if (GUI.Button(new Rect(0.7f * Screen.width, 330, 150, 35), "正常行驶")&&start)
>         {
>             mycar.transform.Find("normal").gameObject.GetComponent<ParticleSystem>().Play();
>             mycar.transform.Find("broken").gameObject.GetComponent<ParticleSystem>().Stop();
>         }
>         if (GUI.Button(new Rect(0.7f * Screen.width, 210, 150, 35), "爆炸"))
>         {
>             boom.transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>().Play();
>             Destroy(mycar);
>         }
>     }
> }
> 
> ```
>



5. **设计小车移动脚本**

> 由于官方给出的小车是没有办法自己控制移动的，而我们上次作业又做了巡逻兵的游戏，所以就复用过来，也做了小车的移动脚本。
>
> **具体代码如下**：
>
> ```c#
> public class car : MonoBehaviour
> {
>        // Start is called before the first frame update
>        void Start()
>        {
>            
>        }
>    
>        // Update is called once per frame
>        void Update()
>     {
>            float KeyVertical = Input.GetAxis("Vertical");
>            float KeyHorizontal = Input.GetAxis("Horizontal");
>            Vector3 newDir = new Vector3(KeyHorizontal, 0, KeyVertical).normalized;
>            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
>            {
>                transform.forward = Vector3.Lerp(transform.forward, newDir, 1);
>            }
>            transform.position += newDir * Time.deltaTime * 3;
>        }
>    }
>    
>    ```





**效果展示**

https://github.com/yaody7/unity3d-learning/blob/master/HW8/movie/展示视频.mp4



Github：https://github.com/yaody7/unity3d-learning/tree/master/HW8




