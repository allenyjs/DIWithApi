# <center>整理個人常用的CallAPI與Autofac結合實作範例</center> #

>**DIWithApi.Service**
實作CallAPI的部分

>**DIWithApi.Model**
對應API的Model

>**DIWithApi.Interface**
約定Service要實做哪些方法

>**DIWithApi**
>>**註冊**
使用Autofac在Global.asax的Application_Start中加入AutofacRegistration方法，做註冊的動作

>>**HomeController**
在HomeController的建構式注入已經寫好的Service