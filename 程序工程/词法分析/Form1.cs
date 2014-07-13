using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
namespace 词法分析
{
    
    public partial class Form1 : Form
    {
        class Grammer
        {
            String left;

            public String Left
            {
                get { return left; }
                set { left = value; }
            }
            List<String> right;

            public List<String> Right
            {
                get { return right; }
                set { right = value; }
            }
            List<String> select;

            public List<String> Select
            {
                get { return select; }
                set { select = value; }
            }
            public Grammer(String left, List<String> right, List<String> select)
            {
                this.left = left;
                this.right = right;
                this.select = select;
            }
        }
        class Vn
        {
            List<Grammer> grammers;

            public List<Grammer> Grammers
            {
                get { return grammers; }
                set { grammers = value; }
            }
            List<String> follow;

            public List<String> Follow
            {
                get { return follow; }
                set { follow = value; }
            }
            public Vn(List<Grammer> grammers, List<String> follow)
            {
                this.grammers = grammers;
                this.follow = follow;
            }
        }
        class Areas
        {
            int id;
            int parent;
            int start;
            int stop;
            public Areas(int id, int parent, int start, int stop)
            {
                this.id = id;
                this.parent = parent;
                this.start = start;
                this.stop = stop;
            }
        }
        class Chars
        {
            String name;

            public String Name
            {
                get { return name; }
                set { name = value; }
            }
            String type;

            public String Type
            {
                get { return type; }
                set { type = value; }
            }
            String kind;

            public String Kind
            {
                get { return kind; }
                set { kind = value; }
            }
            String value;

            public String Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
            String addr;

            public String Addr
            {
                get { return addr; }
                set { addr = value; }
            }
            public Chars(String name, String type, String kind, String value, String addr)
            {
                this.name = name;
                this.type = type;
                this.kind = kind;
                this.value = value;
                this.addr = addr;
            }
            
        }
        class Words
        {
            String content;
            String typecode;
            String shuxing;
            public Words(String content, String typecode, String shuxing)
            {
                this.content = content;
                this.typecode = typecode;
                this.shuxing = shuxing;
            }
            public String getContent()
            {
                return this.content;
            }
            public String getTypecode()
            {
                return this.typecode;
            }
            public String getShuxing()
            {
                return this.shuxing;
            }
        }
       
        String inputString;
        int inputNum;
        bool ifcontain;
        String currentString;
        List<Words> GJZ = new List<Words>();//关键字
        List<Words> SJF = new List<Words>();//双界符
        List<Words> DJF = new List<Words>();//单界符
        List<int> errorrows = new List<int>();//错误行数
        List<Chars> FHB = new List<Chars>();//符号表
        List<Areas> ZYY = new List<Areas>();//作用域;
        List<String> TriAdd = new List<String>();//三地址码
        Vn A;
        Vn B;
        Vn C;
        Vn D;
        Vn E;
        Vn F;
        Vn G;
        Vn H;
        Vn I;
        Vn J;
        Vn K;
        Vn L;
        Vn M;
        Vn N;
        Vn O;
        Vn P;
        Vn Q;
        Vn R;
        Vn S;
        Vn T;
        Vn U;
        Vn V;
        Vn W;
        Vn X;
        Vn Y;
        Vn Z;
        Grammer S1;
        Grammer S2;
        Grammer S3;
        Grammer S4;
        Grammer S5;
        Grammer S6;
        Grammer V1;
        Grammer V2;
        Grammer B1;
        Grammer D1;
        Grammer D2;
        Grammer D3;
        Grammer D4;
        Grammer D5;
        Grammer D6;
        Grammer D7;
        Grammer D8;
        Grammer K1;
        Grammer K2;
        Grammer I1;
        Grammer C1;
        Grammer C2;
        Grammer L1;
        Grammer M1;
        Grammer M2;
        Grammer N1;
        Grammer N2;
        Grammer N3;
        Grammer N4;
        Grammer O1;
        Grammer P1;
        Grammer P2;
        Grammer P3;
        Grammer P4;
        Grammer P5;
        Grammer Q1;
        Grammer E1;
        Grammer E2;
        Grammer R1;
        Grammer R2;
        Grammer J1;
        Grammer J2;
        Grammer U1;
        Grammer A1;
        Grammer A2;
        Grammer A3;
        Grammer W1;
        Grammer G1;
        Grammer G2;
        Grammer G3;
        Grammer T1;
        Grammer H1;
        Grammer H2;
        Grammer H3;
        Grammer F1;
        Grammer F2;
        Grammer F3;
        Grammer F4;
        Grammer F5;
        Grammer F6;
        Grammer F7;
        Grammer F8;
        Grammer F9;
        Grammer X1;
        Grammer X2;
        Grammer X3;
        Grammer X4;
        Grammer X5;
        Grammer X6;
        Grammer Y1;
        Grammer Y2;
        Grammer Z1;
        Grammer Z2;
        IEnumerable<String> rightS1 = new String[] { "B", "V" };
        IEnumerable<String> rightS2 = new String[] { "I",";", "V" };
        IEnumerable<String> rightS3 = new String[] { "O", "V" };
        IEnumerable<String> rightS4 = new String[] { "W", "V" };
        IEnumerable<String> rightS5 = new String[] { "R", "V" };
        IEnumerable<String> rightS6 = new String[] { "Q", "V" };
        IEnumerable<String> rightV1 = new String[] { "S" };
        IEnumerable<String> rightV2 = new String[] { "NULL" };
        IEnumerable<String> rightB1 = new String[] { "FUNC", "D", "IDN", "(", "I", "K", ")", "L" };
        IEnumerable<String> rightD1 = new String[] { "INT" };
        IEnumerable<String> rightD2 = new String[] { "FLOAT" };
        IEnumerable<String> rightD3 = new String[] { "DOUBLE" };
        IEnumerable<String> rightD4 = new String[] { "STRING" };
        IEnumerable<String> rightD5 = new String[] { "CHAR" };
        IEnumerable<String> rightD6 = new String[] { "LONG" };
        IEnumerable<String> rightD7 = new String[] { "VOID" };
        IEnumerable<String> rightD8 = new String[] { "BOOL" };
        IEnumerable<String> rightK1 = new String[] { ",", "I", "K" };
        IEnumerable<String> rightK2 = new String[] { "NULL" };
        IEnumerable<String> rightI1 = new String[] { "D", "C" };
        IEnumerable<String> rightC1 = new String[] { "IDN" };
        IEnumerable<String> rightC2 = new String[] { "ARRAY" };
        IEnumerable<String> rightL1 = new String[] { "{", "M", "}" };
        IEnumerable<String> rightM1 = new String[] { "N", "M" };
        IEnumerable<String> rightM2 = new String[] { "NULL" };
        IEnumerable<String> rightN1 = new String[] { "O" };
        IEnumerable<String> rightN2 = new String[] { "Q" };
        IEnumerable<String> rightN3 = new String[] { "R" };
        IEnumerable<String> rightN4 = new String[] { "I", ";" };
        IEnumerable<String> rightO1 = new String[] { "FUZHI", "C", "P", "Y" };
        IEnumerable<String> rightY1 = new String[] { "W",";" };
        IEnumerable<String> rightY2 = new String[] { "{", "F", "Z", "}",";" };
        IEnumerable<String> rightZ1 = new String[] { ",", "F", "Z" };
        IEnumerable<String> rightZ2 = new String[] { "NULL" };
        IEnumerable<String> rightP1 = new String[] { "+=" };
        IEnumerable<String> rightP2 = new String[] { "-=" };
        IEnumerable<String> rightP3 = new String[] { "*=" };
        IEnumerable<String> rightP4 = new String[] { "/=" };
        IEnumerable<String> rightP5 = new String[] { "=" };
        IEnumerable<String> rightQ1 = new String[] { "IF", "(", "U", ")", "L", "E" };
        IEnumerable<String> rightE1 = new String[] { "ELSE", "L" };
        IEnumerable<String> rightE2 = new String[] { "NULL" };
        IEnumerable<String> rightR1 = new String[] { "WHILE", "(", "U", ")", "L" };
        IEnumerable<String> rightR2 = new String[] { "FOR", "(", "O", "U",";", "IDN", "J", ")", "L" };
        IEnumerable<String> rightJ1 = new String[] { "++" };
        IEnumerable<String> rightJ2 = new String[] { "--" };
        IEnumerable<String> rightU1 = new String[] { "W", "X", "W", "A" };
        IEnumerable<String> rightA1 = new String[] { "&&", "U" };
        IEnumerable<String> rightA2 = new String[] { "||", "U" };
        IEnumerable<String> rightA3 = new String[] { "NULL" };
        IEnumerable<String> rightW1 = new String[] { "T", "G" };
        IEnumerable<String> rightG1 = new String[] { "+", "T", "G" };
        IEnumerable<String> rightG2 = new String[] { "-", "T", "G" };
        IEnumerable<String> rightG3 = new String[] { "NULL" };
        IEnumerable<String> rightT1 = new String[] { "F", "H" };
        IEnumerable<String> rightH1 = new String[] { "*", "F", "H" };
        IEnumerable<String> rightH2 = new String[] { "/", "F", "H" };
        IEnumerable<String> rightH3 = new String[] { "NULL" };
        IEnumerable<String> rightF1 = new String[] { "(", "W", ")" };
        IEnumerable<String> rightF2 = new String[] { "IDN" };
        IEnumerable<String> rightF3 = new String[] { "INUM" };
        IEnumerable<String> rightF4 = new String[] { "FNUM" };
        IEnumerable<String> rightF5 = new String[] { "STR" };
        IEnumerable<String> rightF6 = new String[] { "CH" };
        IEnumerable<String> rightF7 = new String[] { "ARRAY" };
        IEnumerable<String> rightF8 = new String[] { "TRUE" };
        IEnumerable<String> rightF9 = new String[] { "FALSE" };
        IEnumerable<String> rightX1 = new String[] { "==" };
        IEnumerable<String> rightX2 = new String[] { "!=" };
        IEnumerable<String> rightX3 = new String[] { ">" };
        IEnumerable<String> rightX4 = new String[] { ">=" };
        IEnumerable<String> rightX5 = new String[] { "<" };
        IEnumerable<String> rightX6 = new String[] { "<=" };
        IEnumerable<String> selectS1 = new String[] { "FUNC" };
        IEnumerable<String> selectS2 = new String[] { "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL" };
        IEnumerable<String> selectS3 = new String[] { "FUZHI" };
        IEnumerable<String> selectS4 = new String[] { "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE" };
        IEnumerable<String> selectS5 = new String[] { "WHILE", "FOR" };
        IEnumerable<String> selectS6 = new String[] { "IF" };
        IEnumerable<String> selectV1 = new String[] { "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> selectV2 = new String[] { "#" };
        IEnumerable<String> selectB1 = new String[] { "FUNC" };
        IEnumerable<String> selectD1 = new String[] { "INT" };
        IEnumerable<String> selectD2 = new String[] { "FLOAT" };
        IEnumerable<String> selectD3 = new String[] { "DOUBLE" };
        IEnumerable<String> selectD4 = new String[] { "STRING" };
        IEnumerable<String> selectD5 = new String[] { "CHAR" };
        IEnumerable<String> selectD6 = new String[] { "LONG" };
        IEnumerable<String> selectD7 = new String[] { "VOID" };
        IEnumerable<String> selectD8 = new String[] { "BOOL" };
        IEnumerable<String> selectK1 = new String[] { "," };
        IEnumerable<String> selectK2 = new String[] { "#", ")" };
        IEnumerable<String> selectI1 = new String[] { "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL" };
        IEnumerable<String> selectC1 = new String[] { "IDN" };
        IEnumerable<String> selectC2 = new String[] { "ARRAY" };
        IEnumerable<String> selectL1 = new String[] { "{" };
        IEnumerable<String> selectM1 = new String[] { "FUZHI", "IF", "WHILE", "FOR", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL" };
        IEnumerable<String> selectM2 = new String[] { "#","}"};
        IEnumerable<String> selectN1 = new String[] { "FUZHI" };
        IEnumerable<String> selectN2 = new String[] { "IF" };
        IEnumerable<String> selectN3 = new String[] { "WHILE", "FOR" };
        IEnumerable<String> selectN4 = new String[] { "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL" };
        IEnumerable<String> selectO1 = new String[] { "FUZHI" };
        IEnumerable<String> selectY1 = new String[] { "(", "IDN", "INUM", "FNUM", "STR", "CH", "ARRAY" };
        IEnumerable<String> selectY2 = new String[] { "{" };
        IEnumerable<String> selectZ1 = new String[] { "," };
        IEnumerable<String> selectZ2 = new String[] { "#","}" };
        IEnumerable<String> selectP1 = new String[] { "+=" };
        IEnumerable<String> selectP2 = new String[] { "-=" };
        IEnumerable<String> selectP3 = new String[] { "*=" };
        IEnumerable<String> selectP4 = new String[] { "/=" };
        IEnumerable<String> selectP5 = new String[] { "=" };
        IEnumerable<String> selectQ1 = new String[] { "IF" };
        IEnumerable<String> selectE1 = new String[] { "ELSE" };
        IEnumerable<String> selectE2 = new String[] { "#", "}", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> selectR1 = new String[] { "WHILE" };
        IEnumerable<String> selectR2 = new String[] { "FOR" };
        IEnumerable<String> selectJ1 = new String[] { "++" };
        IEnumerable<String> selectJ2 = new String[] { "--" };
        IEnumerable<String> selectU1 = new String[] { "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE" };
        IEnumerable<String> selectA1 = new String[] { "&&" };
        IEnumerable<String> selectA2 = new String[] { "||" };
        IEnumerable<String> selectA3 = new String[] { "#", ")", "IDN" ,";"};
        IEnumerable<String> selectW1 = new String[] { "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE" };
        IEnumerable<String> selectG1 = new String[] { "+" };
        IEnumerable<String> selectG2 = new String[] { "-" };
        IEnumerable<String> selectG3 = new String[] { ";","#", ")", "==", "!=", ">", ">=", "<", "<=", "IDN", "&&", "||", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "INUM", "FNUM", "STR", "CH", "WHILE", "FOR", "IF" };
        IEnumerable<String> selectT1 = new String[] { "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE" };
        IEnumerable<String> selectH1 = new String[] { "*" };
        IEnumerable<String> selectH2 = new String[] { "/" };
        IEnumerable<String> selectH3 = new String[] { ";", "#", "}", ")", "==", "!=", ">", ">=", "<", "<=", "IDN", "&&", "||", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "INUM", "FNUM", "STR", "CH", "WHILE", "FOR", "IF", "+", "-" };
        IEnumerable<String> selectF1 = new String[] { "(" };
        IEnumerable<String> selectF2 = new String[] { "IDN" };
        IEnumerable<String> selectF3 = new String[] { "INUM" };
        IEnumerable<String> selectF4 = new String[] { "FNUM" };
        IEnumerable<String> selectF5 = new String[] { "STR" };
        IEnumerable<String> selectF6 = new String[] { "CH" };
        IEnumerable<String> selectF7 = new String[] { "ARRAY" };
        IEnumerable<String> selectF8 = new String[] { "TRUE" };
        IEnumerable<String> selectF9 = new String[] { "FALSE" };
        IEnumerable<String> selectX1 = new String[] { "==" };
        IEnumerable<String> selectX2 = new String[] { "!=" };
        IEnumerable<String> selectX3 = new String[] { ">" };
        IEnumerable<String> selectX4 = new String[] { ">=" };
        IEnumerable<String> selectX5 = new String[] { "<" };
        IEnumerable<String> selectX6 = new String[] { "<=" };
        IEnumerable<String> followS = new String[] { "#" };
        IEnumerable<String> followV = new String[] { "#" };
        IEnumerable<String> followB = new String[] { "#", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> followD = new String[] { "#", "IDN" };
        IEnumerable<String> followK = new String[] { "#", ")" };
        IEnumerable<String> followI = new String[] { "#", ";", ",", ")", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> followC = new String[] { "#", ";", ",", ")", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" ,"+=","-=","*=","/=","="};
        IEnumerable<String> followL = new String[] { "#", "}", "ELSE", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> followM = new String[] { "#", "}" };
        IEnumerable<String> followN = new String[] { "#", "}", "FUZHI", "IF", "WHILE", "FOR", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL" };
        IEnumerable<String> followO = new String[] { "#", "}", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "WHILE", "FOR", "IF" };
        IEnumerable<String> followY = new String[] { "#", "}", "(", "IDN", "INUM", "FNUM", "STR", "CH", "ARRAY", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "WHILE", "FOR", "IF" };
        IEnumerable<String> followZ = new String[] { "#","}" };
        IEnumerable<String> followP = new String[] { "#", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE" };
        IEnumerable<String> followQ = new String[] { "#", "}", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> followE = new String[] { "#", "}", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> followR = new String[] { "#", "}", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE", "WHILE", "FOR", "IF" };
        IEnumerable<String> followJ = new String[] { "#", ")" };
        IEnumerable<String> followU = new String[] { "#", ")", "IDN" ,";"};
        IEnumerable<String> followA = new String[] { "#", ")", "IDN" ,";"};
        IEnumerable<String> followW = new String[] {";","#", "}", ")", "==", "!=", ">", ">=", "<", "<=", "IDN", "&&", "||", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "INUM", "FNUM", "STR", "CH", "WHILE", "FOR", "IF" };
        IEnumerable<String> followG = new String[] {";","#", ")", "==", "!=", ">", ">=", "<", "<=", "IDN", "&&", "||", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "INUM", "FNUM", "STR", "CH", "WHILE", "FOR", "IF" };
        IEnumerable<String> followT = new String[] { ";","#", "}", ")", "==", "!=", ">", ">=", "<", "<=", "IDN", "&&", "||", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "INUM", "FNUM", "STR", "CH", "WHILE", "FOR", "IF", "+", "-" };
        IEnumerable<String> followH = new String[] { ";","#", "}", ")", "==", "!=", ">", ">=", "<", "<=", "IDN", "&&", "||", "FUNC", "INT", "FLOAT", "DOUBLE", "STRING", "CHAR", "LONG", "VOID", "BOOL", "FUZHI", "(", "INUM", "FNUM", "STR", "CH", "WHILE", "FOR", "IF", "+", "-" };
        IEnumerable<String> followF = new String[] { "#", "*", "/" ,","};
        IEnumerable<String> followX = new String[] { "#", "(", "IDN", "INUM", "FNUM", "STR", "CH","ARRAY","TRUE","FALSE" };

        List<String> input = new List<String>();
        Stack<String> thinkstack = new Stack<String>();
        public Form1()
        {

            GJZ.Add(new Words("if", "IF", "-"));
            GJZ.Add(new Words("true", "TRUE", "-"));
            GJZ.Add(new Words("false", "FALSE", "-"));
            GJZ.Add(new Words("static", "STATIC", "-"));
            GJZ.Add(new Words("while", "WHILE", "-"));
            GJZ.Add(new Words("do", "DO", "-"));
            GJZ.Add(new Words("sizeof", "volatile", "-"));
            GJZ.Add(new Words("goto", "GOTO", "-"));
            GJZ.Add(new Words("default", "DEFAULT", "-"));
            GJZ.Add(new Words("void", "VOID", "-"));
            GJZ.Add(new Words("signed", "SIGNED", "-"));
            GJZ.Add(new Words("for", "FOR", "-"));
            GJZ.Add(new Words("continue", "CONTINUE", "-"));
            GJZ.Add(new Words("unsigned", "UNSIGNED", "-"));
            GJZ.Add(new Words("short", "SHORT", "-"));
            GJZ.Add(new Words("float", "FLOAT", "-"));
            GJZ.Add(new Words("const", "CONST", "-"));
            GJZ.Add(new Words("union", "UNION", "-"));
            GJZ.Add(new Words("return", "RETURN", "-"));
            GJZ.Add(new Words("auto","AUTO","-"));
            GJZ.Add(new Words("double","DOUBLE","-"));
            GJZ.Add(new Words("int","INT","-"));
            GJZ.Add(new Words("struct","STRUCT","-"));
            GJZ.Add(new Words("break","BREAK","-"));
            GJZ.Add(new Words("else", "ELSE", "-"));
            GJZ.Add(new Words("long", "LONG", "-"));
            GJZ.Add(new Words("switch", "SWITCH", "-"));
            GJZ.Add(new Words("case", "CASE", "-"));
            GJZ.Add(new Words("enum", "ENUM", "-"));
            GJZ.Add(new Words("register", "REGISTER", "-"));
            GJZ.Add(new Words("typedef", "TYPEDEF", "-"));
            GJZ.Add(new Words("char", "CHAR", "-"));
            GJZ.Add(new Words("string", "STRING", "-"));
            GJZ.Add(new Words("extern", "EXTERN", "-"));
            GJZ.Add(new Words("Func", "FUNC", "-"));
            GJZ.Add(new Words("fuzhi", "FUZHI", "-"));
            SJF.Add(new Words(":=",":=","-"));
            SJF.Add(new Words("!=", "!=", "-"));
            SJF.Add(new Words("<=", "<=", "-"));
            SJF.Add(new Words(">=", ">=", "-"));
            SJF.Add(new Words("++", "++", "-"));
            SJF.Add(new Words("--", "--", "-"));
            SJF.Add(new Words("==", "==", "-"));
            SJF.Add(new Words("+=", "+=", "-"));
            SJF.Add(new Words("-=", "-=", "-"));
            SJF.Add(new Words("*=", "*=", "-"));
            SJF.Add(new Words("/=", "/=", "-"));
            SJF.Add(new Words("&&", "&&", "-"));
            SJF.Add(new Words("||", "||", "-"));
            SJF.Add(new Words("->", "指针", "-"));
            DJF.Add(new Words("=", "=", "-"));
            DJF.Add(new Words("+", "+", "-"));
            DJF.Add(new Words("-", "-", "-"));
            DJF.Add(new Words("*", "*", "-"));
            DJF.Add(new Words("/", "/", "-"));
            DJF.Add(new Words(";", ";", "-"));
            DJF.Add(new Words(":", ":", "-"));
            DJF.Add(new Words(",", ",", "-"));
            DJF.Add(new Words("(", "(", "-"));
            DJF.Add(new Words(")", ")", "-"));
            DJF.Add(new Words("{", "{", "-"));
            DJF.Add(new Words("}", "}", "-"));
            DJF.Add(new Words("&", "按位与", "-"));
            DJF.Add(new Words("|", "按位或", "-"));
            DJF.Add(new Words("^", "异或", "-"));
            DJF.Add(new Words("?", "?", "-"));
            DJF.Add(new Words("!", "!", "-"));
            DJF.Add(new Words("<", "<", "-"));
            DJF.Add(new Words(">", ">", "-"));
            DJF.Add(new Words("_", "下划线", "-"));
            DJF.Add(new Words("[", "[", "-"));
            DJF.Add(new Words("]", "]", "-"));
            //DJF.Add(new Words("'", "单引号", "-"));
            //DJF.Add(new Words("\"", "双引号", "-"));


            S1 = new Grammer("S", new List<String>(rightS1), new List<String>(selectS1));
            S2 = new Grammer("S", new List<String>(rightS2), new List<String>(selectS2));
            S3 = new Grammer("S", new List<String>(rightS3), new List<String>(selectS3));
            S4 = new Grammer("S", new List<String>(rightS4), new List<String>(selectS4));
            S5 = new Grammer("S", new List<String>(rightS5), new List<String>(selectS5));
            S6 = new Grammer("S", new List<String>(rightS6), new List<String>(selectS6));
            V1 = new Grammer("V", new List<String>(rightV1), new List<String>(selectV1));
            V2 = new Grammer("V", new List<String>(rightV2), new List<String>(selectV2));
            B1 = new Grammer("B", new List<String>(rightB1), new List<String>(selectB1));
            D1 = new Grammer("D", new List<String>(rightD1), new List<String>(selectD1));
            D2 = new Grammer("D", new List<String>(rightD2), new List<String>(selectD2));
            D3 = new Grammer("D", new List<String>(rightD3), new List<String>(selectD3));
            D4 = new Grammer("D", new List<String>(rightD4), new List<String>(selectD4));
            D5 = new Grammer("D", new List<String>(rightD5), new List<String>(selectD5));
            D6 = new Grammer("D", new List<String>(rightD6), new List<String>(selectD6));
            D7 = new Grammer("D", new List<String>(rightD7), new List<String>(selectD7));
            D8 = new Grammer("D", new List<String>(rightD8), new List<String>(selectD8));
            K1 = new Grammer("K", new List<String>(rightK1), new List<String>(selectK1));
            K2 = new Grammer("K", new List<String>(rightK2), new List<String>(selectK2));
            I1 = new Grammer("I", new List<String>(rightI1), new List<String>(selectI1));
            C1 = new Grammer("C", new List<String>(rightC1), new List<String>(selectC1));
            C2 = new Grammer("C", new List<String>(rightC2), new List<String>(selectC2));
            L1 = new Grammer("L", new List<String>(rightL1), new List<String>(selectL1));
            M1 = new Grammer("M", new List<String>(rightM1), new List<String>(selectM1));
            M2 = new Grammer("M", new List<String>(rightM2), new List<String>(selectM2));
            N1 = new Grammer("N", new List<String>(rightN1), new List<String>(selectN1));
            N2 = new Grammer("N", new List<String>(rightN2), new List<String>(selectN2));
            N3 = new Grammer("N", new List<String>(rightN3), new List<String>(selectN3));
            N4 = new Grammer("N", new List<String>(rightN4), new List<String>(selectN4));
            O1 = new Grammer("O", new List<String>(rightO1), new List<String>(selectO1));
            Y1 = new Grammer("Y", new List<String>(rightY1), new List<String>(selectY1));
            Y2 = new Grammer("Y", new List<String>(rightY2), new List<String>(selectY2));
            Z1 = new Grammer("Z", new List<String>(rightZ1), new List<String>(selectZ1));
            Z2 = new Grammer("Z", new List<String>(rightZ2), new List<String>(selectZ2));
            P1 = new Grammer("P", new List<String>(rightP1), new List<String>(selectP1));
            P2 = new Grammer("P", new List<String>(rightP2), new List<String>(selectP2));
            P3 = new Grammer("P", new List<String>(rightP3), new List<String>(selectP3));
            P4 = new Grammer("P", new List<String>(rightP4), new List<String>(selectP4));
            P5 = new Grammer("P", new List<String>(rightP5), new List<String>(selectP5));
            Q1 = new Grammer("Q", new List<String>(rightQ1), new List<String>(selectQ1));
            E1 = new Grammer("E", new List<String>(rightE1), new List<String>(selectE1));
            E2 = new Grammer("E", new List<String>(rightE2), new List<String>(selectE2));
            R1 = new Grammer("R", new List<String>(rightR1), new List<String>(selectR1));
            R2 = new Grammer("R", new List<String>(rightR2), new List<String>(selectR2));
            J1 = new Grammer("J", new List<String>(rightJ1), new List<String>(selectJ1));
            J2 = new Grammer("J", new List<String>(rightJ2), new List<String>(selectJ2));
            U1 = new Grammer("U", new List<String>(rightU1), new List<String>(selectU1));
            A1 = new Grammer("A", new List<String>(rightA1), new List<String>(selectA1));
            A2 = new Grammer("A", new List<String>(rightA2), new List<String>(selectA2));
            A3 = new Grammer("A", new List<String>(rightA3), new List<String>(selectA3));
            W1 = new Grammer("W", new List<String>(rightW1), new List<String>(selectW1));
            G1 = new Grammer("G", new List<String>(rightG1), new List<String>(selectG1));
            G2 = new Grammer("G", new List<String>(rightG2), new List<String>(selectG2));
            G3 = new Grammer("G", new List<String>(rightG3), new List<String>(selectG3));
            T1 = new Grammer("T", new List<String>(rightT1), new List<String>(selectT1));
            H1 = new Grammer("H", new List<String>(rightH1), new List<String>(selectH1));
            H2 = new Grammer("H", new List<String>(rightH2), new List<String>(selectH2));
            H3 = new Grammer("H", new List<String>(rightH3), new List<String>(selectH3));
            F1 = new Grammer("F", new List<String>(rightF1), new List<String>(selectF1));
            F2 = new Grammer("F", new List<String>(rightF2), new List<String>(selectF2));
            F3 = new Grammer("F", new List<String>(rightF3), new List<String>(selectF3));
            F4 = new Grammer("F", new List<String>(rightF4), new List<String>(selectF4));
            F5 = new Grammer("F", new List<String>(rightF5), new List<String>(selectF5));
            F6 = new Grammer("F", new List<String>(rightF6), new List<String>(selectF6));
            F7 = new Grammer("F", new List<String>(rightF7), new List<String>(selectF7));
            F8 = new Grammer("F", new List<String>(rightF8), new List<String>(selectF8));
            F9 = new Grammer("F", new List<String>(rightF9), new List<String>(selectF9));
            X1 = new Grammer("X", new List<String>(rightX1), new List<String>(selectX1));
            X2 = new Grammer("X", new List<String>(rightX2), new List<String>(selectX2));
            X3 = new Grammer("X", new List<String>(rightX3), new List<String>(selectX3));
            X4 = new Grammer("X", new List<String>(rightX4), new List<String>(selectX4));
            X5 = new Grammer("X", new List<String>(rightX5), new List<String>(selectX5));
            X6 = new Grammer("X", new List<String>(rightX6), new List<String>(selectX6));
            IEnumerable<Grammer> grammerS = new Grammer[] { S1, S2, S3, S4, S5, S6 };
            IEnumerable<Grammer> grammerV = new Grammer[] { V1, V2 };
            IEnumerable<Grammer> grammerB = new Grammer[] { B1 };
            IEnumerable<Grammer> grammerD = new Grammer[] { D1, D2, D3, D4, D5, D6, D7, D8 };
            IEnumerable<Grammer> grammerK = new Grammer[] { K1, K2, };
            IEnumerable<Grammer> grammerI = new Grammer[] { I1 };
            IEnumerable<Grammer> grammerC = new Grammer[] { C1,C2 };
            IEnumerable<Grammer> grammerL = new Grammer[] { L1 };
            IEnumerable<Grammer> grammerM = new Grammer[] { M1,M2};
            IEnumerable<Grammer> grammerN = new Grammer[] { N1, N2, N3, N4 };
            IEnumerable<Grammer> grammerO = new Grammer[] { O1 };
            IEnumerable<Grammer> grammerP = new Grammer[] { P1, P2, P3, P4, P5 };
            IEnumerable<Grammer> grammerQ = new Grammer[] { Q1 };
            IEnumerable<Grammer> grammerE = new Grammer[] { E1, E2 };
            IEnumerable<Grammer> grammerR = new Grammer[] { R1, R2 };
            IEnumerable<Grammer> grammerJ = new Grammer[] { J1, J2 };
            IEnumerable<Grammer> grammerU = new Grammer[] { U1 };
            IEnumerable<Grammer> grammerA = new Grammer[] { A1, A2, A3 };
            IEnumerable<Grammer> grammerW = new Grammer[] { W1 };
            IEnumerable<Grammer> grammerG = new Grammer[] { G1, G2, G3 };
            IEnumerable<Grammer> grammerT = new Grammer[] { T1 };
            IEnumerable<Grammer> grammerH = new Grammer[] { H1, H2, H3 };
            IEnumerable<Grammer> grammerF = new Grammer[] { F1, F2, F3, F4, F5, F6,F7,F8,F9};
            IEnumerable<Grammer> grammerX = new Grammer[] { X1, X2, X3, X4, X5, X6 };
            IEnumerable<Grammer> grammerY = new Grammer[] { Y1, Y2 };
            IEnumerable<Grammer> grammerZ = new Grammer[] { Z1, Z2 };
            A = new Vn(new List<Grammer>(grammerA), new List<String>(followA));
            B = new Vn(new List<Grammer>(grammerB), new List<String>(followB));
            C = new Vn(new List<Grammer>(grammerC), new List<String>(followC));
            D = new Vn(new List<Grammer>(grammerD), new List<String>(followD));
            E = new Vn(new List<Grammer>(grammerE), new List<String>(followE));
            F = new Vn(new List<Grammer>(grammerF), new List<String>(followF));
            G = new Vn(new List<Grammer>(grammerG), new List<String>(followG));
            H = new Vn(new List<Grammer>(grammerH), new List<String>(followH));
            I = new Vn(new List<Grammer>(grammerI), new List<String>(followI));
            J = new Vn(new List<Grammer>(grammerJ), new List<String>(followJ));
            K = new Vn(new List<Grammer>(grammerK), new List<String>(followK));
            L = new Vn(new List<Grammer>(grammerL), new List<String>(followL));
            M = new Vn(new List<Grammer>(grammerM), new List<String>(followM));
            N = new Vn(new List<Grammer>(grammerN), new List<String>(followN));
            O = new Vn(new List<Grammer>(grammerO), new List<String>(followO));
            P = new Vn(new List<Grammer>(grammerP), new List<String>(followP));
            Q = new Vn(new List<Grammer>(grammerQ), new List<String>(followQ));
            R = new Vn(new List<Grammer>(grammerR), new List<String>(followR));
            S = new Vn(new List<Grammer>(grammerS), new List<String>(followS));
            T = new Vn(new List<Grammer>(grammerT), new List<String>(followT));
            U = new Vn(new List<Grammer>(grammerU), new List<String>(followU));
            V = new Vn(new List<Grammer>(grammerV), new List<String>(followV));
            W = new Vn(new List<Grammer>(grammerW), new List<String>(followW));
            X = new Vn(new List<Grammer>(grammerX), new List<String>(followX));
            Y = new Vn(new List<Grammer>(grammerY), new List<String>(followY));
            Z = new Vn(new List<Grammer>(grammerZ), new List<String>(followZ));
            

            FileStream fs = new FileStream("tokenlist.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < GJZ.Count; i++)
            {
                if(GJZ[i].getContent().Length>7)
                    sw.WriteLine(GJZ[i].getContent()+"\t\t("+GJZ[i].getTypecode()+","+GJZ[i].getShuxing()+")");
                else
                    sw.WriteLine(GJZ[i].getContent() + "\t\t\t(" + GJZ[i].getTypecode() + "," + GJZ[i].getShuxing() + ")");
                sw.Flush();
            }
            for (int i = 0; i < DJF.Count; i++)
            {
                if (DJF[i].getContent().Length > 7)
                    sw.WriteLine(DJF[i].getContent() + "\t\t(" + DJF[i].getTypecode() + ",\t" + DJF[i].getShuxing() + ")");
                else
                    sw.WriteLine(DJF[i].getContent() + "\t\t\t(" + DJF[i].getTypecode() + ",\t\t" + DJF[i].getShuxing() + ")");
                sw.Flush();
            }
            for (int i = 0; i < SJF.Count; i++)
            {
                if (SJF[i].getContent().Length > 7)
                    sw.WriteLine(SJF[i].getContent() + "\t\t(" + SJF[i].getTypecode() + ",\t" + SJF[i].getShuxing() + ")");
                else
                    sw.WriteLine(SJF[i].getContent() + "\t\t\t(" + SJF[i].getTypecode() + ",\t\t" + SJF[i].getShuxing() + ")");
                sw.Flush();
            }
            //关闭流
            sw.Close();
            fs.Close();
            
            InitializeComponent();
            ColumnHeader name = new ColumnHeader();
            ColumnHeader type = new ColumnHeader();
            ColumnHeader kind = new ColumnHeader();
            ColumnHeader value = new ColumnHeader();
            ColumnHeader addr = new ColumnHeader();
            ColumnHeader input = new ColumnHeader();
            ColumnHeader stack = new ColumnHeader();
            ColumnHeader output = new ColumnHeader();
            ColumnHeader tochn = new ColumnHeader();
            name.Text = "Name";
            type.Text = "Type";
            kind.Text = "Kind";
            value.Text = "Value";
            addr.Text = "Addr";
            input.Text = "   输入缓冲区   ";
            stack.Text = "  推导函数串  ";
            output.Text = "  产生式输出  ";
            tochn.Text = " 说明";
            listView1.Columns.AddRange(new ColumnHeader[] { name,type,kind,value,addr});
            listView1.View = View.Details;
            listView2.Columns.AddRange(new ColumnHeader[] { input,stack,output,tochn});
            foreach (ColumnHeader ch in listView2.Columns) { ch.Width = -2; }
            listView2.View = View.Details;

        }
        Vn stringtovn(String s)
        {

            switch (s)
            {
                case "A":
                    return A;
                    break;
                case "B":
                    return B;
                    break;
                case "C":
                    return C;
                    break;
                case "D":
                    return D;
                    break;
                case "E":
                    return E;
                    break;
                case "F":
                    return F;
                    break;
                case "G":
                    return G;
                    break;
                case "H":
                    return H;
                    break;
                case "I":
                    return I;
                    break;
                case "J":
                    return J;
                    break;
                case "K":
                    return K;
                    break;
                case "L":
                    return L;
                    break;
                case "M":
                    return M;
                    break;
                case "N":
                    return N;
                    break;
                case "O":
                    return O;
                    break;
                case "P":
                    return P;
                    break;
                case "Q":
                    return Q;
                    break;
                case "R":
                    return R;
                    break;
                case "S":
                    return S;
                    break;
                case "T":
                    return T;
                    break;
                case "U":
                    return U;
                    break;
                case "V":
                    return V;
                    break;
                case "W":
                    return W;
                    break;
                case "X":
                    return X;
                    break;
                case "Y":
                    return Y;
                    break;
                case "Z":
                    return Z;
                    break;
                default:
                    return null;
                    break;
            }
        }
        void Play()
        {
            StreamReader objReader = new StreamReader("out.txt", Encoding.GetEncoding("gb2312"));
            String sLine = "";
            input.Clear();
            thinkstack.Clear();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                {
                    input.Add(sLine);
                }
            }
            objReader.Close();
            input.Add("#");
            thinkstack.Push("#");
            thinkstack.Push("S");

            FileStream fs = new FileStream("语法分析.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("输入缓冲区\t\t\t推导符号串\t\t\t输出");
            String cash = "";
            List<string> grammeroutlist = new List<string>();
            for (int k = 0; k < input.Count; k++)
            {
                cash += input[k];
            }
            grammeroutlist.Add(cash);
            //cash += "\t\t\t\t";
            cash = "";
            for (int k = 0; k < thinkstack.Count; k++)
            {
                cash += thinkstack.ElementAt(k);
            }
            grammeroutlist.Add(cash);
            sw.WriteLine(grammeroutlist[0]+"\t\t\t\t"+grammeroutlist[1]);
            listView2.Items.Add(new ListViewItem(new string[] { grammeroutlist[0],grammeroutlist[1],"","" }, -2));
            while (thinkstack.Count > 1)
            {
                String lookfor = input[0]; 
                switch (lookfor)
                {
                    case "INT":
                        if (input.Count > 2 && input[1].Length>3 && input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "int";
                                }
                            }
                        }
                        else if (input.Count > 2 && input[1].Length>5&& input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "int";
                                }
                            }
                        }
                        break;
                    case "DOUBLE":
                        if (input.Count > 2&& input[1].Length>3 && input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "double";
                                }
                            }
                        }
                        else if (input.Count > 2 && input[1].Length>5&& input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "double";
                                }
                            }
                        }
                        break;
                    case "FLOAT":
                        if (input.Count > 2&& input[1].Length>3 && input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "float";
                                }
                            }
                        }
                        else if (input.Count > 2&& input[1].Length>5 && input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "float";
                                }
                            }
                        }
                        break;
                    case "STRING":
                        if (input.Count > 2 && input[1].Length>3&& input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "string";
                                }
                            }
                        }
                        else if (input.Count > 2 && input[1].Length>5&& input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "string";
                                }
                            }
                        }
                        break;
                    case "CHAR":
                        if (input.Count > 2 && input[1].Length>3&& input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "char";
                                }
                            }
                        }
                        else if (input.Count > 2&& input[1].Length>5 && input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "char";
                                }
                            }
                        }
                        break;
                    case "LONG":
                        if (input.Count > 2 && input[1].Length>3&& input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "long";
                                }
                            }
                        }
                        else if (input.Count > 2&& input[1].Length>5 && input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "long";
                                }
                            }
                        }
                        break;
                    case "VOID":
                        if (input.Count > 2&& input[1].Length>3 && input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "void";
                                }
                            }
                        }
                        else if (input.Count > 2&& input[1].Length>5 && input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "void";
                                }
                            }
                        }
                        break;
                    case "BOOL":
                        if (input.Count > 2&& input[1].Length>3 && input[1].Substring(0, 3) == "IDN")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(4))
                                {
                                    FHB[i].Type = "bool";
                                }
                            }
                        }
                        else if (input.Count > 2 && input[1].Length>5&& input[1].Substring(0, 5) == "ARRAY")
                        {
                            for (int i = 0; i < FHB.Count; i++)
                            {
                                if (FHB[i].Name == input[1].Substring(6))
                                {
                                    FHB[i].Type = "bool";
                                }
                            }
                        }
                        break;
                    default:
                        if (lookfor.Length>3&&lookfor.Substring(0, 3) == "IDN")
                        {
                            lookfor = "IDN";
                        }
                        else if (lookfor.Length > 5 && lookfor.Substring(0, 5) == "ARRAY")
                        {
                            lookfor = "ARRAY";
                        }
                        else if (lookfor.Length > 4 && lookfor.Substring(0, 4) == "INUM")
                        {
                            lookfor = "INUM";
                        }
                        else if (lookfor.Length > 4 && lookfor.Substring(0, 4) == "FNUM")
                        {
                            lookfor = "FNUM";
                        }
                        break;
                }
                String output = "";
                grammeroutlist.Clear();
                if (stringtovn(thinkstack.Peek()) != null)
                {
                    Vn topvn = stringtovn(thinkstack.Peek());
                    bool bingo = false;
                    for (int i = 0; i < topvn.Grammers.Count && bingo == false; i++)
                    {
                        for (int j = 0; j < topvn.Grammers[i].Select.Count && bingo == false; j++)
                        {
                            if (lookfor == topvn.Grammers[i].Select[j])
                            {
                                bingo = true;
                                List<String> newlist = stringtovn(thinkstack.Pop()).Grammers[i].Right;
                                if (newlist[0] != "NULL")
                                {
                                    for (int k = newlist.Count - 1; k >= 0; k--)
                                    {
                                        thinkstack.Push(newlist[k]);
                                    }
                                }
                                
                                for (int k = 0; k < input.Count; k++)
                                {
                                    output += input[k];
                                }
                                grammeroutlist.Add(output);
                                //if (grammeroutlist[0].Length > 7)
                                //    output += "\t\t\t";
                                //else
                                //    output += "\t\t\t\t";
                                output = "";
                                for (int k = 0; k < thinkstack.Count; k++)
                                {
                                    output += thinkstack.ElementAt(k);
                                }
                                grammeroutlist.Add(output);
                                //if (output.Length - length.Length > 7)
                                //    output += "\t\t\t";
                                //else
                                //    output += "\t\t\t\t";
                                output = "";
                                output += topvn.Grammers[i].Left + "→";
                                for (int k = 0; k < topvn.Grammers[i].Right.Count; k++)
                                {
                                    output += topvn.Grammers[i].Right[k];
                                }
                                grammeroutlist.Add(output);
                                output = "";
                                if (topvn.Grammers[i].Right.Contains("B"))
                                {
                                    output += " 函数";
                                }
                                if (topvn.Grammers[i].Right.Contains("I"))
                                {
                                    output += " 变量说明";
                                }
                                if (topvn.Grammers[i].Right.Contains("O"))
                                {
                                    output += " 赋值语句";
                                }
                                if (topvn.Grammers[i].Right.Contains("Q"))
                                {
                                    output += " 条件语句";
                                }
                                if (topvn.Grammers[i].Right.Contains("R"))
                                {
                                    output += " 循环语句";
                                }
                                if (topvn.Grammers[i].Right.Contains("W"))
                                {
                                    output += " 表达式";
                                }
                                if (topvn.Grammers[i].Right.Contains("D"))
                                {
                                    output += " 类型";
                                }
                                if (topvn.Grammers[i].Right.Contains("L"))
                                {
                                    output += " 语句块";
                                }
                                if (topvn.Grammers[i].Right.Contains("M"))
                                {
                                    output += " 语句串";
                                }
                                if (topvn.Grammers[i].Right.Contains("N"))
                                {
                                    output += " 语句";
                                }
                                if (topvn.Grammers[i].Right.Contains("U"))
                                {
                                    output += " 条件";
                                }
                                grammeroutlist.Add(output);
                                listView2.Items.Add(new ListViewItem(new string[] { grammeroutlist[0], grammeroutlist[1], grammeroutlist[2], grammeroutlist[3] }, -2));
                            }
                        }
                    }
                    for (int i = 0; i < topvn.Follow.Count && bingo == false; i++)
                    {
                        if (topvn.Follow[i] == lookfor)
                        {
                            bingo = true;
                            thinkstack.Pop();
                            for (int k = 0; k < input.Count; k++)
                            {
                                output += input[k];
                            }
                            grammeroutlist.Add(output);
                            //string length = output;
                            //if (length.Length > 7)
                            //    output += "\t\t\t";
                            //else
                            //    output += "\t\t\t\t";
                            //length = output;
                            output = "";
                            for (int k = 0; k < thinkstack.Count; k++)
                            {
                                output += thinkstack.ElementAt(k);
                            }
                            grammeroutlist.Add(output);
                            //if (output.Length - length.Length > 7)
                            //    output += "\t\t\t";
                            //else
                            //    output += "\t\t\t\t";
                            //output += "错误";
                            grammeroutlist.Add("错误");
                            listView2.Items.Add(new ListViewItem(new string[] { grammeroutlist[0], grammeroutlist[1], grammeroutlist[2], "" }, -2));
                        }
                    }
                    if (bingo == false)
                    {
                        String badboy = "";
                        badboy = lookfor;
                        //if(badboy=="#"){

                        //}
                        input.RemoveAt(0);
                        for (int k = 0; k < input.Count; k++)
                        {
                            output += input[k];
                        }
                        grammeroutlist.Add(output);
                        //string length = output;
                        //if (length.Length > 7)
                        //    output += "\t\t\t";
                        //else
                        //    output += "\t\t\t\t";
                        //length = output;
                        output = "";
                        for (int k = 0; k < thinkstack.Count; k++)
                        {
                            output += thinkstack.ElementAt(k);
                        }
                        grammeroutlist.Add(output);
                        //if (output.Length - length.Length > 7)
                        //    output += "\t\t\t";
                        //else
                        //    output += "\t\t\t\t";
                        output = "";
                        output += "忽略" + badboy;
                        grammeroutlist.Add(output);
                        listView2.Items.Add(new ListViewItem(new string[] { grammeroutlist[0], grammeroutlist[1], grammeroutlist[2], "" }, -2));
                    }
                }
                else
                {
                    bool error = false;
                    if (input[0] != "#")
                    {
                        input.RemoveAt(0);
                    }
                    else
                    {
                        error = true;
                    }
                    thinkstack.Pop();
                    for (int k = 0; k < input.Count; k++)
                    {
                        output += input[k];
                    }
                    grammeroutlist.Add(output);
                    //string length = output;
                    //if (length.Length > 7)
                    //    output += "\t\t\t";
                    //else
                    //    output += "\t\t\t\t";
                    //length = output;
                    output = "";
                    for (int k = 0; k < thinkstack.Count; k++)
                    {
                        output += thinkstack.ElementAt(k);
                    }
                    grammeroutlist.Add(output);
                    if (error == true)
                    {
                        grammeroutlist.Add("错误");
                    }
                    if (grammeroutlist.Count<3)
                    {
                        grammeroutlist.Add("");
                    }
                    listView2.Items.Add(new ListViewItem(new string[] { grammeroutlist[0], grammeroutlist[1], grammeroutlist[2], "" }, -2));
                }
                //sw.WriteLine(output);
            }


            sw.Close();
            fs.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("out.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string stringtobin = "";
            textBox4.Text = "";
            errorrows.Clear();
            FHB.Clear();
            ZYY.Clear();
            TriAdd.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            int first = 0, length = 1,linenum=1;
            inputString = textBox1.Text;
            String tokenstring=null,ADDR=null;
            bool token = false,wantone=false;
            Match m;
            Regex biaoshifu = new Regex("^[A-Za-z_]+[A-Za-z_0-9]*$"); //标识符
            Regex shuzu = new Regex("^[A-Za-z_]+[A-Za-z_0-9]*(\\[\\d+\\])+$"); //数组
            Regex error1shuzu = new Regex("^[A-Za-z_]+[A-Za-z_0-9]*\\[?\\d?\\]?$"); //未完成的数组
            Regex zhengchangshu = new Regex("^-?\\d+$");//整数
            Regex fudianchangshu = new Regex("^(-?\\d+)(\\.\\d+)?$");//浮点常数
            Regex zifuchangshu = new Regex("'\\\\?\\w'");//字符常数
            Regex zifuchuan = new Regex("^\"\\w*\"$");//字符串
            Regex error1zifuchangshu = new Regex("'\\\\?\\w+[^']$");//错误的字符常数1
            Regex error2zifuchangshu = new Regex("'\\\\?\\w+'");//错误的字符常数2
            Regex error3zifuchangshu = new Regex("^'\\\\?\\w?$");//错误的字符常数3
            Regex error1biaoshifu = new Regex("^[0-9]+[A-Za-z]+[0-9]*$");//错误的标识符
            Regex error1zifuchuan = new Regex("^\"\\w*[^\"]$");//错误的字符串常量
            Regex danhangzhushi = new Regex(@"\/\/[^\r\n]*(\r\n)*", RegexOptions.Singleline);//单行注释
            Regex duohangzhushi = new Regex(@"/\*(\s|.)*?\*/", RegexOptions.Multiline);//多行注释
            Regex zuoyongyu = new Regex(@"^((for|while)\\(\\w*\\))?\\{\\w*\\}$", RegexOptions.Multiline);//作用域
            Regex blsm = new Regex("(int|float|double|bool|void|char|string|long)\\s+[A-Za-z_]+[A-Za-z_0-9]*\\s*;"); //变量声明
            Regex gcfh = new Regex("return\\s+[A-Za-z_]+[A-Za-z_0-9]*;"); //过程返回
            Regex szcs = new Regex("(int|float|double|bool|void|char|string|long)\\s+[A-Za-z_]+[A-Za-z_0-9]*,?"); //实在参数
            Regex gcdy = new Regex("Func\\s+(int|float|double|bool|void|char|string|long)\\s+[A-Za-z_]+[A-Za-z_0-9]*"); //过程调用
            Regex fzyj = new Regex("fuzhi\\s+[A-Za-z_]+[A-Za-z_0-9]*\\s*=\\s*\\S+\\s*;"); //赋值语句
            Regex tjzyyj = new Regex("if\\s*\\(\\S+\\)\\s*\\r*\\{\\s*\\r*\\S*\\s*\\r*\\}"); //条件转移语句
            textBox2.Text = "";
            textBox3.Text = "";
            m = danhangzhushi.Match(inputString);
            while(m.Success)
            {
                inputString = inputString.Substring(0, m.Index)+ inputString.Substring(m.Index+m.Length);
                m = danhangzhushi.Match(inputString);
            }
            m = duohangzhushi.Match(inputString);
            while (m.Success)
            {
                inputString = inputString.Substring(0, m.Index)+ inputString.Substring(m.Index+m.Length);
                m = duohangzhushi.Match(inputString);
            }
            string tempinput = inputString;
            m = gcdy.Match(tempinput);
            while (m.Success)
            {
                string tempstring = tempinput.Substring(m.Index, m.Length) ;
                Regex fname = new Regex("[A-Za-z_]+[A-Za-z_0-9]*\\(");
                Match m2 = fname.Match(tempinput);
                if (tempinput.Contains('(') && tempinput.Contains(')')&&m2.Success)
                {
                    string param = tempinput.Substring(tempinput.IndexOf('('), tempinput.IndexOf(')') - tempinput.IndexOf('(') + 1);
                    int num = 0;
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (param[i] == ',')
                            num++;
                    }
                    num++;
                    tempstring = "call " + tempinput.Substring(m2.Index, m2.Length - 1) + " " + num + "\r\n";

                    TriAdd.Add(tempstring);
                }
                tempinput = tempinput.Substring(0, m.Index) + tempinput.Substring(m.Index + m.Length);
                m = gcdy.Match(tempinput);
            }
            m = fzyj.Match(tempinput);
            while (m.Success)
            {
                string tempstring = tempinput.Substring(m.Index, m.Length - 1) ;
                tempstring = tempstring.Substring(tempstring.IndexOf(' ')) + "\r\n";
                TriAdd.Add(tempstring);
                tempinput = tempinput.Substring(0, m.Index) + tempinput.Substring(m.Index + m.Length);
                m = fzyj.Match(tempinput);
            }
            m = blsm.Match(tempinput);
            while (m.Success)
            {
                string tempstring = tempinput.Substring(m.Index, m.Length-1) + "\r\n";
                TriAdd.Add(tempstring);
                tempinput = tempinput.Substring(0, m.Index) + tempinput.Substring(m.Index + m.Length);
                m = blsm.Match(tempinput);
            }
            m = gcfh.Match(tempinput);
            while (m.Success)
            {
                string tempstring = tempinput.Substring(m.Index, m.Length) + "\r\n";
                TriAdd.Add(tempstring);
                tempinput = tempinput.Substring(0, m.Index) + tempinput.Substring(m.Index + m.Length);
                m = gcfh.Match(tempinput);
            }
            m = szcs.Match(tempinput);
            while (m.Success)
            {
                string tempstring = tempinput.Substring(m.Index, m.Length);
                tempstring = "param" + tempstring.Substring(tempstring.LastIndexOf(' '));
                if (tempstring.IndexOf(',')!=-1)
                    tempstring = tempstring.Substring(0, tempstring.Length - 1)+"\r\n";
                else
                    tempstring = tempstring.Substring(0, tempstring.Length) + "\r\n";
                TriAdd.Add(tempstring);
                tempinput = tempinput.Substring(0, m.Index) + tempinput.Substring(m.Index + m.Length);
                m = szcs.Match(tempinput);
            }
            m = tjzyyj.Match(tempinput);
            while (m.Success)
            {
                string tempstring = tempinput.Substring(m.Index, m.Length - 1);
                tempstring = "if x relop y goto l\r\n";
                TriAdd.Add(tempstring);
                tempinput = tempinput.Substring(0, m.Index) + tempinput.Substring(m.Index + m.Length);
                m = tjzyyj.Match(tempinput);
            }


            
            inputNum = inputString.Length;
            //m = zuoyongyu.Match(inputString);
            //while (m.Success)
            //{
            //    ZYY.Add(new Areas(ZYY.Count,0,m.Index,m.Index+m.Length-1));
            //}

            for (int i = 0; i < inputNum; i++)
            {
                
                token = false;
                char onemore = inputString[i];

                currentString = inputString.Substring(first, length);

                if (onemore == ' '||onemore=='\n')
                {
                    token = true;
                    first = i + 1;
                    length = 1;
                    if (onemore == '\n')
                    {
                        linenum++;
                    }
                    continue;
                }
                foreach(Words w in GJZ){
                    if (w.getContent().Equals(currentString)&&token==false)
                    {
                        if (i != inputNum - 1)
                        {

                            if ((inputString[i + 1] >= 'a' && inputString[i + 1] <= 'z') || (inputString[i + 1] <= 'Z' && inputString[i + 1] >= 'A') || (inputString[i + 1] >= '0' && inputString[i + 1] <= '9'))
                            {
                                //length++;
                                token = false;
                                wantone = true;
                                continue;
                            }
     
                            else
                            {
                                tokenstring = w.getContent() + "\t" + w.getTypecode() + "\t" + w.getShuxing() + "\r\n";
                                textBox2.Text += tokenstring;
                                sw.WriteLine(w.getTypecode());
                                first += length;
                                length = 1;
                                token = true;
                                continue;
                            }
                        }
                        tokenstring = w.getContent() + "\t" + w.getTypecode() + "\t" + w.getShuxing() + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine(w.getTypecode());
                        first += length;
                        length = 1;
                        token = true;
                        continue;
                    }
                }
                foreach (Words w in SJF)
                {
                    if (w.getContent().Equals(currentString) && token == false)
                    { 
                        tokenstring = w.getContent() + "\t" + w.getTypecode() + "\t" + w.getShuxing() + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine(w.getTypecode());
                        first += length;
                        length = 1;
                        token = true;
                        continue;
                    }
                }
                foreach (Words w in DJF)
                {
                    if (w.getContent().Equals(currentString) && token == false)
                    {
                        if (i != inputNum - 1)
                        {

                            if (inputString[i + 1] == '=' || inputString[i + 1] == '-' || inputString[i + 1] == '+' || inputString[i + 1] == '&' || inputString[i + 1] == '|' || inputString[i + 1] == '>')
                            {
                                //length++;
                                wantone = true;
                                continue;
                            }
                            if (onemore == '_')
                            {
                                wantone = true;
                                continue;
                            }
                            else
                            {
                                tokenstring = w.getContent() + "\t" + w.getTypecode() + "\t" + w.getShuxing() + "\r\n";
                                textBox2.Text += tokenstring;
                                sw.WriteLine(w.getTypecode());
                                first += length;
                                length = 1;
                                token = true;
                                continue;
                            }
                        }
                        tokenstring = w.getContent() + "\t" + w.getTypecode() + "\t" + w.getShuxing() + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine(w.getTypecode());
                        first += length;
                        length = 1;
                        token = true;
                        continue;
                    }
                }
                if (token == false)
                {
                    m = biaoshifu.Match(currentString); // 在字符串中匹配
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if ((inputString[i + 1] >= 'a' && inputString[i + 1] <= 'z') || (inputString[i + 1] <= 'Z' && inputString[i + 1] >= 'A') || (inputString[i + 1] >= '0' && inputString[i + 1] <= '9') || inputString[i + 1] == '[' || inputString[i + 1]==']'||inputString[i+1]=='_')
                            {
                                wantone = true;
                                length++;
                                continue;
                            }
                            else
                            {
                                ifcontain = false;
                                
                                for (int j = 0; j < FHB.Count; j++)
                                {
                                    if (FHB[j].Name == currentString)
                                    {
                                        ifcontain = true;
                                        ADDR = FHB[j].Addr;
                                    }
                                }
                                
                                if (ifcontain == false)
                                {
                                    ADDR = FHB.Count.ToString();
                                    FHB.Add(new Chars(currentString, null, "简变", null, FHB.Count.ToString()));
                                }
                                tokenstring = currentString + "\t" + "IDN" + "\t" + ADDR + "\r\n";
                                textBox2.Text += tokenstring;
                                sw.WriteLine("IDN:"+currentString);
                                token = true;
                                first += length;
                                length = 1;
                                continue;
                            }
                        }
                        ifcontain = false;
                        for (int j = 0; j < FHB.Count; j++)
                        {
                            if (FHB[j].Name == currentString)
                            {
                                ifcontain = true;
                                ADDR = FHB[j].Addr;
                            }
                        }

                        if (ifcontain == false)
                        {
                            ADDR = FHB.Count.ToString();
                            FHB.Add(new Chars(currentString, null, "简变", null, FHB.Count.ToString()));
                        }
                        tokenstring = currentString + "\t" + "IDN" + "\t" + ADDR + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine("IDN:" + currentString);
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = shuzu.Match(currentString); // 在字符串中匹配
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if ((inputString[i + 1] >= 'a' && inputString[i + 1] <= 'z') || (inputString[i + 1] <= 'Z' && inputString[i + 1] >= 'A') || (inputString[i + 1] >= '0' && inputString[i + 1] <= '9') || inputString[i + 1] == '[' || inputString[i + 1] == ']' || inputString[i + 1] == '_')
                            {
                                wantone = true;
                                length++;
                                continue;
                            }
                            else
                            {
                                ifcontain = false;
                                for (int j = 0; j < FHB.Count; j++)
                                {
                                    if (FHB[j].Name == currentString)
                                    {
                                        ADDR = FHB[j].Addr;
                                        ifcontain = true;
                                    }
                                }
                                if (ifcontain == false)
                                {
                                    ADDR = FHB.Count.ToString();
                                    FHB.Add(new Chars(currentString, null, "数组", null, FHB.Count.ToString()));
                                }
                                tokenstring = currentString + "\t" + "ARRAY" + "\t" + ADDR + "\r\n";
                                textBox2.Text += tokenstring;
                                sw.WriteLine("ARRAY:"+currentString);
                                token = true;
                                first += length;
                                length = 1;
                                continue;
                            }
                        }
                        ifcontain = false;
                        for (int j = 0; j < FHB.Count; j++)
                        {
                            if (FHB[j].Name == currentString)
                            {
                                ADDR = FHB[j].Addr;
                                ifcontain = true;
                            }
                        }
                        if (ifcontain == false)
                        {
                            ADDR = FHB.Count.ToString();
                            FHB.Add(new Chars(currentString, null, "数组", null, FHB.Count.ToString()));
                        }
                        tokenstring = currentString + "\t" + "ARRAY" + "\t" + ADDR + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine("ARRAY:"+currentString);
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = error1biaoshifu.Match(currentString); // 在字符串中匹配
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if ((inputString[i + 1] >= 'a' && inputString[i + 1] <= 'z') || (inputString[i + 1] <= 'Z' && inputString[i + 1] >= 'A') || (inputString[i + 1] >= '0' && inputString[i + 1] <= '9'))
                            {
                                length++;
                                continue;
                            }
                            else
                            {
                                tokenstring = currentString + "\t" + "错误的标识符" + "\t" + linenum + "行\r\n";
                                textBox3.Text += tokenstring;
                                token = true;
                                first += length;
                                length = 1;
                                continue;
                            }
                        }
                        tokenstring = currentString + "\t" + "错误的标识符" + "\t" + linenum + "行\r\n";
                        textBox3.Text += tokenstring;
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = zhengchangshu.Match(currentString); 
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if (inputString[i + 1] >= '0' && inputString[i + 1] <= '9')
                            {
                                length++;
                                continue;
                            }
                            else if (inputString[i + 1] == '.')
                            {
                                token = false;
                                length++;
                                continue;
                            }
                            else if ((inputString[i + 1] >= 'a' && inputString[i + 1] <= 'z') || (inputString[i + 1] <= 'Z' && inputString[i + 1] >= 'A'))
                            {
                                token = false;
                                length++;
                                continue;
                            }
                            else if (inputString[i + 1] == ' ')
                            {
                                ifcontain = false;
                                for (int j = 0; j < FHB.Count; j++)
                                {
                                    if (FHB[j].Name == currentString)
                                    {
                                        ifcontain = true;
                                    }
                                }
                                if (ifcontain == false)
                                {
                                    byte[] bytearr = System.Text.Encoding.Default.GetBytes(currentString);
                                    for (int n = 0; n < bytearr.Length; n++)
                                    {
                                        stringtobin += bytearr[n].ToString();
                                    }
                                    FHB.Add(new Chars(currentString, "整型", "常数", stringtobin, FHB.Count.ToString()));
                                }
                                tokenstring = currentString + "\t" + "INUM" + "\t" + stringtobin + "\r\n";
                                textBox2.Text += tokenstring;
                                sw.WriteLine("INUM:"+currentString);
                                token = true;
                                first += length;
                                length = 1;
                                continue;
                            }
                        }
                        ifcontain = false;
                        for (int j = 0; j < FHB.Count; j++)
                        {
                            if (FHB[j].Name == currentString)
                                ifcontain = true;
                        }
                        if (ifcontain == false)
                        {
                            byte[] bytearr = System.Text.Encoding.Default.GetBytes(currentString);
                            for (int n = 0; n < bytearr.Length; n++)
                            {
                                stringtobin +=   bytearr[n].ToString();
                            }
                            FHB.Add(new Chars(currentString, "整型", "常数", stringtobin, FHB.Count.ToString()));
                        }
                        tokenstring = currentString + "\t" + "INUM" + "\t" + stringtobin + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine("INUM:"+currentString);
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = fudianchangshu.Match(currentString);
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if (inputString[i + 1] >= '0' && inputString[i + 1] <= '9')
                            {
                                length++;
                                continue;
                            }
                            else if (inputString[i + 1] == '.')
                            {
                                token = false;
                                length++;
                                continue;
                            }
                            else if ((inputString[i + 1] >= 'a' && inputString[i + 1] <= 'z') || (inputString[i + 1] <= 'Z' && inputString[i + 1] >= 'A'))
                            {
                                token = false;
                                length++;
                                continue;
                            }
                            else
                            {
                                ifcontain=false;
                                for (int j = 0; j < FHB.Count; j++)
                                {
                                    if (FHB[j].Name == currentString)
                                        ifcontain = true;
                                }
                                
                                if(ifcontain==false)
                                {
                                    
                                    byte[] bytearr = System.Text.Encoding.Default.GetBytes(currentString);
                                    for (int n = 0; n < bytearr.Length; n++)
                                    {
                                        stringtobin +=   bytearr[n].ToString();
                                    }
                                    FHB.Add(new Chars(currentString, "浮点型", "常数", stringtobin, FHB.Count.ToString()));
                                }
                                tokenstring = currentString + "\t" + "FNUM" + "\t" + stringtobin + "\r\n";
                                textBox2.Text += tokenstring;
                                sw.WriteLine("FNUM:"+currentString);
                                token = true;
                                first += length;
                                length = 1;
                                continue;
                            }
                        }
                        ifcontain = false;
                        for (int j = 0; j < FHB.Count; j++)
                        {
                            if (FHB[j].Name == currentString)
                                ifcontain = true;
                        }
                        if (ifcontain == false)
                        {
                            byte[] bytearr = System.Text.Encoding.Default.GetBytes(currentString);
                            for (int n = 0; n < bytearr.Length; n++)
                            {
                                stringtobin += bytearr[n].ToString();
                            }
                            FHB.Add(new Chars(currentString, "浮点型", "常数", stringtobin, FHB.Count.ToString()));
                        }
                        tokenstring = currentString + "\t" + "FNUM" + "\t" + stringtobin + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine("FNUM:"+currentString);
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = zifuchangshu.Match(currentString);
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        ifcontain = false;
                        for (int j = 0; j < FHB.Count; j++)
                        {
                            if (FHB[j].Name == currentString)
                                ifcontain = true;
                        }
                        if (ifcontain == false)
                        {
                            byte[] bytearr = System.Text.Encoding.Default.GetBytes(currentString);
                            for (int n = 0; n < bytearr.Length; n++)
                            {
                                stringtobin += bytearr[n].ToString();
                            }
                            FHB.Add(new Chars(currentString, "字符", "常数", stringtobin, FHB.Count.ToString()));
                        }
                        tokenstring = currentString + "\t" + "CH" + "\t" + stringtobin + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine("CH:"+currentString);
                        token = true;
                    
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = zifuchuan.Match(currentString);
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        ifcontain = false;
                        for (int j = 0; j < FHB.Count; j++)
                        {
                            if (FHB[j].Name == currentString)
                                ifcontain = true;
                        }
                        if (ifcontain == false)
                        {
                            byte[] bytearr = System.Text.Encoding.Default.GetBytes(currentString);
                            for (int n = 0; n < bytearr.Length; n++)
                            {
                                stringtobin +=   bytearr[n].ToString();
                            }
                            FHB.Add(new Chars(currentString, "字符串", "常数", stringtobin, FHB.Count.ToString()));
                        }
                        tokenstring = currentString + "\t" + "STR" + "\t" + stringtobin + "\r\n";
                        textBox2.Text += tokenstring;
                        sw.WriteLine("STR:"+currentString);
                        token = true;
                        first += length;
                        length = 1;
                        textBox3.Text = "";
                        errorrows.Clear();
                        continue;
                    }
                    m = error1zifuchuan.Match(currentString);
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if (inputString[i + 1] != '"')
                            {
                                tokenstring = currentString + "\t" + "未完成的字符串" + "\t" + linenum + "行\r\n";
                                textBox3.Text = tokenstring;
                                token = true;
                                wantone = true;
                                length++;
                                continue;
                            }
                            else
                            {

                                length++;
                                continue;
                            }
                        }
                        if (onemore == '"')
                            continue;
                        tokenstring = currentString + "\t" + "未完成的字符串" + "\t" + linenum + "行\r\n";
                        textBox3.Text = tokenstring;
                        token = true;
                        wantone = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = error1zifuchangshu.Match(currentString);
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        if (i != inputNum - 1)
                        {

                            if (inputString[i + 1] !='\'')
                            {
                                tokenstring = currentString + "\t" + "缺少单引号" + "\t" + linenum + "行\r\n";
                                textBox3.Text = tokenstring;
                                token = true;
                                length++;
                                continue;
                            }
                            else
                            {

                                length++;
                                continue;
                            }
                        }
                        tokenstring = currentString + "\t" + "缺少单引号" + "\t" + linenum + "行\r\n";
                        textBox3.Text = tokenstring;
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                    m = error2zifuchangshu.Match(currentString);
                    if (m.Success && m.Index == 0 && m.Length == length)
                    {
                        //if (i != inputNum - 1)
                        //{

                        //    if (inputString[i + 1] != '\'')
                        //    {
                        //        length++;
                        //        continue;
                        //    }
                        //    else
                        //    {
                        //        tokenstring = currentString + "\t" + "错误的字符常数2" + "\t" + currentString + "\r\n";
                        //        textBox3.Text = tokenstring;
                        //        token = true;
                        //        first += length;
                        //        length = 1;
                        //        continue;
                        //    }
                        //}
                        tokenstring = currentString + "\t" + "错误的字符常数" + "\t" + linenum + "行\r\n";
                        textBox3.Text = tokenstring;
                        token = true;
                        first += length;
                        length = 1;
                        continue;
                    }
                }
                m = error3zifuchangshu.Match(currentString);
                if (m.Success && m.Index == 0 && m.Length == length)
                {
                    if (i != inputNum - 1)
                    {

                        if (inputString[i + 1] != ' ')
                        {
                            length++;
                            continue;
                        }
                        else
                        {
                            tokenstring = currentString + "\t" + "未完成的字符常数" + "\t" + linenum + "行\r\n";
                            textBox3.Text = tokenstring;
                            token = true;
                            first += length;
                            length = 1;
                            wantone = true;
                            continue;
                        }
                    }
                    tokenstring = currentString + "\t" + "未完成字符常数" + "\t" + linenum + "行\r\n";
                    textBox3.Text = tokenstring;
                    token = true;
                    first += length;
                    length = 1;
                    wantone = true;
                    continue;
                }
                if (token == false)
                {
                    length++;//不匹配任何项，自动加一
                    if (onemore != '\r' && wantone == false&&(!errorrows.Contains(linenum)))
                           errorrows.Add(linenum);
                }
            }
            for(int i=0;i<errorrows.Count;i++)
            {
                textBox3.Text += "第" + errorrows[i] + "行有词法错误\r\n";
            }
            
            
            
            sw.Close();
            fs.Close();
            Play();
            for (int i = 0; i < FHB.Count; i++)
            {
                listView1.Items.Add(new ListViewItem(new string[] { FHB[i].Name, FHB[i].Type, FHB[i].Kind, FHB[i].Value, FHB[i].Addr }, -2));
            }
            for (int i = 0; i < TriAdd.Count; i++)
            {
                textBox4.Text += TriAdd[i];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listView2.Items.Clear();
            //Play();
        }



    }
}
