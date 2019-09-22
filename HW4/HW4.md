# HW4

### 1. 基本操作演练【建议做】

- 下载  Fantasy Skybox FREE， 构建自己的游戏场景

>  **答：**

> 1. 打开U3D界面中间的 **Asset Store** ，在里面寻找Fantasy Skybox Free。
>
> **注意：** 我使用的是2018.3.14f1的版本，如果直接在Editor里面登陆总是会闪烁登不进去，**解决方法** 是在Unity Hub先登陆，这样就可以避开这个“坑”了。
>
> ![1569063769779](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569063769779.png)
>
> 2. 接着，我们在 **Asset Store** 中搜索 **Fantasy Skybox FREE** ，按照介绍导入包，我们的 **Assets** 就会变成这样
>
> ![1569063922111](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569063922111.png)
>
> 3. 我们在新下载的包里面的 **Materials** 文件夹里面，选择我们需要的 **天空盒** 并将其拖到 **Main Camera** 上，这样我们的游戏场景就拥有了炫酷一点的天空了。
> 4. 接着创建一个 **Terrain** ，开始建造我们的地形。在右侧的工具栏可以选择不同的工具调整我们的地形。
>
> ![1569064555935](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569064555935.png)
>
> 5. 成果图：
>
> ![1569064618368](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569064618368.png)
>
> （有点丑……
>
> **注意注意：一块Terrain就很大了，如果电脑不够好的，可能会卡顿**



- 写一个简单的总结，总结游戏对象的使用

> **答：**

> 我们可以在U3D的右侧 **Hierarchy** 创建我们自己需要的 **游戏对象** 。当然，系统自带的游戏对象一般比较简单，如果我们有需要的话，还可以在 **Asset Store** 中找到更多更好看的游戏对象。若是 **Asset Store** 登不上去的话，我们就只能尝试用这些简单游戏对象拼成一个更好看的对象了。**游戏对象** 是我们游戏的核心，我们做的所有操作，都将映射到 **游戏对象** 上。但是不要简单的认为 **游戏对象** 就是我们控制的小人、小车。它也可能是我们的游戏中的背景，我们的背景音乐，这些元素也称为 **游戏对象** 。一开始 **游戏对象** 只是一个简单的图形，或是其他简单的的东西。如何丰富我们的 **游戏对象** ？那就是在 **游戏对象** 上添加组件，比如我们可以添加 **Material** 来改变 **游戏对象** 的颜色，还有很多其他的组件，这不过我现在还不太熟悉U3D，所以还不知道怎么用而已。在我用过的组件中，其中最重要的，我认为就是 **脚本** 了。我们通过编写脚本，并挂载到我们的 **游戏对象** 上，这样我们就可以通过鼠标、键盘等方式来操作我们的游戏对象，这也是我们玩游戏最主要的东西了。
>
> 关于 **游戏对象** 的知识，我的总结大概就是这样。希望通过之后的学习能对 **游戏对象** 甚至对整个U3D有更深的理解。



### 2. 编程实践

	- 牧师与魔鬼 动作分离版

> 因为本次编程和上次的有紧密的联系（就是在上次的基础上做的，所以这里先贴上上次的博客，以及github。
>
> **博客：** https://blog.csdn.net/u011430932/article/details/100934114
>
> **github：** https://github.com/yaody7/unity3d-learning/tree/master/HW3
>
> 接着，我们来看一下这次动作分离版的牧师与魔鬼到底分离在哪里，与上次的区别又在哪里

> 在这版游戏中，我将 **View.cs** 删掉了，取而代之的是 **action_manager.cs** ,与之配套的是 **action.cs** ——用来存储游戏对象的 **动作** 。
>
> ![1569066102087](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569066102087.png)
>
> 我们按脚本文件来查看各部分究竟在做些什么。



#### action.cs

> **action：** 所有动作类的基类， 继承于 **ScriptableObject** 。
>
> **注意：** Scriptable的实例化不能用new，而应该用CreateInstance，例如：
>
> `ship_action s = ScriptableObject.CreateInstance<ship_action>();`
>
> 
>
> **ship_action：** 定义了船的动作。
>
> > **void Awake()：** 在该ScriptableObject创建的时候就调用，里面调用了 **judge_direction** 函数，用以决定船的方向。
> >
> > **void judge_direction()：** 该函数用于决定船的移动方向，并调用船这个类的成员函数 **get_pose** 来帮助决定方向。
> >
> > **bool update()：** 调用船这个类中的 **move** 成员函数控制船和船员的移动，当移动完毕后返回true，否则返回false。
> >
> > **注意：** 因为涉及浮点数比较，所以这里使用了 **Vector3** 的 **Distance** 方法，用以判断船是否到达了目的地。
>
>  
>
> **person_action：**
>
> > **void judge_direction()：** 该函数用于决定牧师或魔鬼的运动方向，而且其中还调用了 **change_passengers** 函数来将成员转移到不同的子位置下。
> >
> > **void change_passenger()：** 该函数用于将船员在 **左岸、船、右岸** 上转移。
> >
> > **bool update()：** 调用人这个类中的 **move** 成员函数控制人的移动，当移动完毕后返回true，否则返回false。



#### action_manageer.cs

> **成员变量：**
>
> - **string show：** 用以记录游戏结束显示框的显示内容
> - **bool click：** 用以表示此时是否可以接受点击事件
> - **person_action current_person_action：** 用以记录此时是否接收到了人物动作
> - **ship_action current_ship_action：** 用以记录此时是否接收到了船的动作
>
> **成员函数：**
>
> > **void Update()：** 处理接收到的动作，在这个函数中，顺便接受了用以接受用户点击人物事件。
> >
> > **注意：** 这里使用了Ray、RaycastHit来探测鼠标点击的gameobject，以下是例子：
> >
> > ```c#
> > Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
> > RaycastHit hit;
> > if(Physics.Raycast(ray,out hit))
> > {
> >     Person temp = find_person(hit.collider.gameObject);
> >     if (temp != null)
> >         {
> >         person_action charu = ScriptableObject.CreateInstance<person_action>();
> >         charu.set_Person(temp);
> >         current_person_action = charu;
> >         click = false;
> >         }
> > }
> > ```
> > 当然，在每次 **Update** 后都要使用 **judge函数** 判断是否游戏结束。
> >
> > **Person find_person(GameObject g)：** 配合 **Update函数** ，确定所点击的对象。
> >
> > **void OnGUI()：** 建立按钮，处理 **开船** 和 **重新开始** 事件，以及显示游戏结束信息。



#### Controller_Script.cs

> 删去了原有的动作的判断以及执行函数。



#### Model.cs

> 大体没有什么改变，值得注意的是。在这版游戏中，我选择将 **move函数** 的实现要基于一帧一帧图像，使得物体移动不是一蹴而就，增加游戏感。



### 3. 材料与渲染联系【可选】

- 从 Unity 5 开始，使用新的 Standard Shader 作为自然场景的渲染。     
  - 阅读官方 [Standard Shader](https://docs.unity3d.com/Manual/shader-StandardShader.html) 手册 。
  - 选择合适内容，如 [Albedo Color and Transparency](https://docs.unity3d.com/Manual/StandardShaderMaterialParameterAlbedoColor.html)，寻找合适素材，用博客展示相关效果的呈现

**答：**

> **Albedo Color and Transparency**
>
> 我们常用Material来设置我们的游戏对象外观。但是，外观除了颜色RGB之外，还有一个透明度的选项。今天就来介绍一下 **Albedo Color and Transparency** 。

> - 首先，我们建立5个material，并在侧面的Albedo栏设置其外观。
>
> ![1569119419534](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569119419534.png)
>
> ![1569119375327](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569119375327.png)
>
> - 接着建立5个Sphere，并将这5个新创建的Material挂上去，效果就会像下图一样。
>
> ![1569119515369](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569119515369.png)

> 以上就是 **Albedo Color and Transparency** 的简单使用了。



- Unity 5 声音     
  - 阅读官方 [Audio](https://docs.unity3d.com/Manual/Audio.html) 手册
  - 用博客给出游戏中利用 Reverb Zones 呈现车辆穿过隧道的声效的案例

**答：**

> - 首先，我们在U3D中加入 **Audio Reverb Zone** 和 **Audio Source** 。
>
> ![1569120769374](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569120769374.png)
>
> - 接着，在**Audio Reverb Zone** 的右侧属性栏设置为 **Cave** 
>
> ![1569120855333](https://github.com/yaody7/unity3d-learning/blob/master/HW4/pics/1569120855333.png)
>
> - 接着，我们需要在 **Audio Source** 加入汽车行驶的声音。我们到 **Asset Store**搜索，但是由于资金有限，无法花 **5刀** 去买一个音频文件。所以就没能真正模拟声音了。

