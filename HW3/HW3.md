# HW3

### 1、简答并用程序验证

- 游戏对象运动的本质是什么？

**答：** 游戏对象运动的本质就是使用矩阵变换（平移、旋转、缩放）改变游戏对象的空间属性。我们做的游戏关键就是游戏对象在每一帧图像上怎么变换。最直观的就是观察我们每个对象的Transform，里面的Position、Rotation以及Scale就是最直接控制我们对象的参数。而游戏对象运动的本质在某种程度上也可以看成是这些参数的变换。

- 请用三种方法以上方法，实现物体的抛物线运动。（如，修改Transform属性，使用向量Vector3的方法…）

**答：** 

> **方法1：**
>
> ```c#
> public float yspeed = 10;
> public float xspeed = 20;
> void Update()
> {
>     float ymove= yspeed * Time.deltaTime + 1 / 2 * (-1) * Time.deltaTime * Time.deltaTime;
>     this.transform.position += Vector3.up * ymove;
>     float xmove = xspeed * Time.deltaTime;
>     Debug.Log(xspeed);
>     Debug.Log(yspeed);
>     this.transform.position += Vector3.left * xmove;
>     yspeed -= 1;
> 
> }
> ```



> **方法2：**
>
> ```c#
> public float yspeed = 10;
> public float xspeed = 20;
> void Update()
> {
>     Vector3 move = new Vector3((-1)*Time.deltaTime * xspeed, yspeed * Time.deltaTime + 1 / 2 * (-1) * Time.deltaTime * Time.deltaTime, 0);
>     this.transform.position += move;
>     yspeed -= 1;
> }
> ```



> **方法3：**
>
> ```c#
> public float yspeed = 10;
> public float xspeed = 20;
> void Update()
> {
>     Vector3 move = new Vector3((-1) * Time.deltaTime * xspeed, yspeed * Time.deltaTime + 1 / 2 * (-1) * Time.deltaTime * Time.deltaTime, 0);
>     this.transform.Translate(move);
>     yspeed -= 1;
> }
> ```



- 写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。

  > **行星脚本：**
  >
  > ```c#
  > public float speed;
  > public float yangle, zangle;
  > void Update()
  > {
  >     Vector3 axis = new Vector3(0, yangle, zangle);
  >     this.transform.RotateAround(new Vector3(0, 0, 0), axis, speed * Time.deltaTime);
  >     this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
  > }
  > ```
  > 使用不同的yangle和zangle设定公转的轴方向，使得行星公转不在同一法平面
  
  > **卫星脚本：**
  >
  > ```c#
  > public Transform earth;
  > void Update()
  > {
  >     this.transform.RotateAround(earth.position, Vector3.up, 10000 * Time.deltaTime);
  >     this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
  > }
  > ```
  > 月球直接设置绕地球转即可，但是公转速度要设大一些，否则可能绕不回来。



### 2、编程实践

  

- 阅读以下游戏脚本


> Priests and Devils
>
> Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There  are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one  boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other  side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the  priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many > ways. Keep all priests alive! Good luck!



**事物：**

![1568709685961](C:\Users\89481\AppData\Roaming\Typora\typora-user-images\1568709685961.png)

> **Devil：** 恶魔，游戏主体，当数量大于牧师时判定为游戏失败。
>
> **Priest：** 牧师，游戏主体，当数量小于恶魔时，判定为游戏失败。
>
> **Directional Light：** 光照源，为所有Object提供光照。
>
> **leftShore：** 左边的海岸，恶魔与牧师的起始海岸。
>
> **rightShore：** 右边的海岸，恶魔与牧师的终止海岸。
>
> **ship：** 牧师与恶魔用以过海的工具。



**规则表：**

> - 点击 **恶魔** 与 **牧师** 可控制其上船、下船。
> - 点击 **开船** 按钮可以使ship移向对岸
> - 点击 **重新开始** 按钮可以重新开始游戏
> - **倒计时** 时间到后，游戏失败，返回开始菜单



**MVC结构：**

> **Model：** 在Model中，我设计了Ship、Person、Shore类，定义了其中的方法以及成员变量。
>
> > **Ship：**
> >
> > - moveLeft()：控制小船左移
> >
> > - moveRight()：控制小船右移
>
> > **Person：**
> >
> > - leftdown()：控制恶魔或牧师在左边上船
> > - leftup()：控制恶魔或牧师在左边上岸
> > - rightdown()：控制恶魔或牧师右边上船
> > - rightup()：控制恶魔或牧师右边上岸
>
> > **Shore类只有简单的构造函数，加载预设，这里不赘述了**



> **View：** 在View中，设计了几个按钮，及其触发的事件。通过告知Controller来做更多的计算。
>
> > **Update：**
> >
> > 在Update中设计了船的移动，通过transform和Vector3的变换，对ship和其中的成员变量Arraylist Passengers来控制船和恶魔、牧师的移动。
>
> > **OnGui：**
> >
> > 在OnGUi中设计了 **开船** 按钮和 **重新开始** 按钮，使用Application.LoadLevel(0)来重置游戏。并设计了获胜和落败的文字显示。



> **Controller：** 在Controller中，用了单例模式来控制游戏的进行。其中包括了船的移动方向，人的移动方向及游戏结束与否判断等方法。
>
> > **loadResource：** 加载预设，创建游戏中的各个Object
>
> > **ship_move：** 控制船的移动
>
> > **judge：** 判断游戏结束与否，胜负条件
>
> > **person_move：** 控制恶魔与牧师的移动



**其他脚本**

> **welcome.cs：** 这是第一场景，也就是开始菜单的脚本，里面包含了规则的介绍，以及一个开始按钮。
>
> **mytime.cs：** 这个脚本使用了 **协程** 来做这个游戏的计时器。以下是其关键代码：
>
> ```c#
> void Start()
> {
>     StartCoroutine(startTime());   
> }
> 
> public IEnumerator startTime()
> {
> 
>     while (TotalTime >= 0)
>     {
>         yield return new WaitForSeconds(1);
>         TotalTime--;
> 
>     }
> }
> ```



### 3. 思考题

**实现RotateAround，代码如下：**

> void RotateAround(Transform t, Vector3 center, Vector3 axis, float angle)
> {
>     var position = t.position;
>     var rot = Quaternion.AngleAxis(angle, axis);
>     var direction = position - center;
>     direction = rot * direction;
>     t.position = center + direction;
>     t.rotation *= rot;
> }



