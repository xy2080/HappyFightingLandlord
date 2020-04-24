using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml.Serialization;
/// <summary>
/// 供外界访问的静态工具类
/// </summary>
public static class ToolsManager  {

    static Transform m_CanvasTransform;

    /// <summary>
    /// 获取Canvas的Transform
    /// </summary>
   public static Transform canvasTransform
    {
        get {
            if (m_CanvasTransform == null)
            {
                m_CanvasTransform = GameObject.Find("Canvas").transform;
            }
            
                return m_CanvasTransform;
        }
    }

    /// <summary>
    /// 根据实例化的预制体的枚举类型，加载同名预制体
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject CreatePrefab(PrefabType type)
    {
        GameObject go = Resources.Load<GameObject>(type.ToString());
        if(go==null)
        {
            return null;
        }
        else
        {
            //在Canvas下实例化物体，并设置不保持世界坐标
            return GameObject.Instantiate(go,canvasTransform,false);
        }
    }
   
    /// <summary>
    /// 卡牌集合的排序
    /// </summary>
    /// <param name="cards">卡牌集合</param>
    /// <param name="asc">是否升序</param>
    public static void  SortList(List<Card> cards,bool asc)
    {
        cards.Sort(
            (Card a,Card b)=>
            {
                if(asc)
                {
                    return a.WeightValue.CompareTo(b.WeightValue);
                }
                else
                {
                    return -a.WeightValue.CompareTo(b.WeightValue);
                }
            }
            );
    }
   
    /// <summary>
    /// 获取出的牌的权值
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="cardType"></param>
    /// <returns></returns>
    public static int  GetWeight(List<Card> cards,CardType cardType)
    {
        int total = 0;

        //三带一或三带二只需要比较连续相同的三张牌
        if (cardType == CardType.ThreeAndOne || cardType == CardType.ThreeAndTwo)
        {
            for (int i = 0; i < cards.Count; i++)
              {             
                if(cards[i].WeightValue== cards[i+1].WeightValue&& cards[i+1].WeightValue== cards[i+2].WeightValue)
                {
                    total +=(int) cards[i].WeightValue;
                    total *= 3;
                    break;
                }               
           
              }
        }
        //顺子和双顺只需要比较升序的起始牌的大小以及集合的长度
        else if (cardType == CardType.ThreeAndOne || cardType == CardType.ThreeAndTwo)
        {
            total = (int)cards[0].WeightValue;
        }
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {               
                    total += (int)cards[i].WeightValue;
            }
        }
        return total;
    }
   
    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data"></param>
    public static void SaveData(GameData data)
    {
        Stream stream = new FileStream(Consts.DataPath,FileMode.OpenOrCreate,FileAccess.Write);//创建写入的流
        StreamWriter sw = new StreamWriter(stream,Encoding.UTF8);///操作流以及格式码
        XmlSerializer xml = new XmlSerializer(data.GetType());//存储的类型
        xml.Serialize(sw,data);//存储
        //关闭流
        stream.Close();
        sw.Close();
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    /// <returns></returns>
    public static GameData GetData()
    {
        GameData data = new GameData();
        Stream stream = new FileStream(Consts.DataPath, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(stream,true);//true不带Bom
        XmlSerializer xml = new XmlSerializer(data.GetType());
        data = xml.Deserialize(sr) as GameData ;
        stream.Close();
        sr.Close();
        return data;
    }
}
