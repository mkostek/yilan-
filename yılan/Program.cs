/*
 * SharpDevelop tarafından düzenlendi.
 * Kullanıcı: abuzer
 * Tarih: 21.8.2018
 * Zaman: 20:46
 * 
 * Bu şablonu değiştirmek için Araçlar | Seçenekler | Kodlama | Standart Başlıkları Düzenle 'yi kullanın.
 */
using System;
using System.Timers;

namespace yılan
{
	class Program
	{
		public static bool yemi=false;//yem yendimi yenmedimi?
		public static int en=20;//haritanın eni
		public static int[,] yılan=new int[50,3];//yılan boyu haritadaki kordinatları
		public static int uzunluk=3;//baslangıc boyu
		public static int boy=20;//haritanın boyunu ayarlayan parametre
		public static int zaman=0,zamani=-1;//timerde gecen zamanı tutmak için tanımlanmıs degerler
		private static int[,] harita = new int[boy, en];//haritayı tanımla
		private static Timer t = new Timer(100);//timer i set ediyoruz

		public static void ilk(){//yılanı ilk boyu
			yılan[1,0]=boy/2;yılan[1,1]=en/2+1;yılan[1,2]=6;
			yılan[2,0]=boy/2;yılan[2,1]=en/2+2;yılan[2,2]=6;
		}
		public static bool kontrol(int yon)//yılan hareketlerinin engelleyen herangi bir şey var mı?
		{
			bool flag=false;//bayrakta değeri tut
			int a=yılan[uzunluk-1,0];//herbir kısmının öznitelikleri
			int b=yılan[uzunluk-1,1];
			switch(yon)
			{

					
				case 2://aşağı hareketi
					if(harita[a+1,b]==0)flag=true;//boşsa hareket edebilir
					else if(harita[a+1,b]==9){//ama eğer yem varsa
						yemi=false;flag=true;uzunluk++;yılan[uzunluk-1,0]=a;yılan[uzunluk-1,1]=b;yılan[uzunluk-1,2]=2;
					}//yem bayrağını değiştir uzunluğu artır yeni gelen öznitelik değerlerini ata(parçaya)
					break;
				case 4:
					if(harita[a,b-1]==0)flag=true;//aynı şeyler bunlar içinde geçerli
					else if(harita[a,b-1]==9){
						yemi=false;flag=true;uzunluk++;yılan[uzunluk-1,0]=a;yılan[uzunluk-1,1]=b;yılan[uzunluk-1,2]=4;

					}break;
				case 6:
					if(harita[a,b+1]==0)flag=true;
					else if(harita[a,b+1]==9){
						yemi=false;flag=true;uzunluk++;yılan[uzunluk-1,0]=a;yılan[uzunluk-1,1]=b;yılan[uzunluk-1,2]=6;
					}break;
				case 8:
					if(harita[a-1,b]==0)flag=true;
					else if(harita[a-1,b]==9){
						yemi=false;flag=true;uzunluk++;yılan[uzunluk-1,0]=a;yılan[uzunluk-1,1]=b;yılan[uzunluk-1,2]=8;
					}
					break;
			}
			return flag;
		}
		public static void yem()//yem yendi mi?
		{
			int a;
			int b;
			Random r=new Random();
			while(!yemi){//yem bayrağı yanlış olduğu sürece
				yemi=true;//yendi bayrağını true et
				a=r.Next(1,boy-1);//rastgele x
				b=r.Next(1,en-1);//rastgele y
				
				for(int j=0;j<uzunluk;j++){//acaba yılanın üstüne yem gelir mi gelirse değiştir
					if(a==yılan[j,0] && yılan[j, 1] == b)
						yemi=false;//bayrağı false le
				}
				if(yemi){//yemi true ise
					harita[a,b]=9;//haritaya yem yazdır
				}
			}
			
			
			
		}
		public static void sürün(int i)//yılan sürükler
		{
			
			switch (i) {//yön bilgisi
				case 2:
					if (kontrol(2) && yılan[uzunluk - 1, 2] != 8) {//2 se ve hareket etmesineengel yoksa
						yılan[uzunluk - 1, 0]++;//yukarı
						yılan[uzunluk - 1, 2] = 2;
					} else {
						t.Stop();//yanlış bir hareket se game over
						Console.Write("Game over..");
					}
					break;
				case 4:
					if (kontrol(4) && yılan[uzunluk - 1, 2] != 6) {//solaysa ve hareketine engel yoksa
						yılan[uzunluk - 1, 1]--;
						yılan[uzunluk - 1, 2] = 4;//sola
					} else {
						t.Stop();//yanlış bir harekette gane over
						Console.Write("Game over..");
					}
					break;
				case 6:
					if (kontrol(6) && yılan[uzunluk - 1, 2] != 4) {
						yılan[uzunluk - 1, 1]++;//sağaysa ve hareketine engel yoksa
						yılan[uzunluk - 1, 2] = 6;
					} else {
						t.Stop();//yanlı bir harekette game over
						Console.Write("Game over..");
					}
					break;
				case 8:
					if (kontrol(8) && yılan[uzunluk - 1, 2] != 2) {//yukarıysa ve hareket etmesine engel yoksa
						yılan[uzunluk - 1, 0]--;
						yılan[uzunluk - 1, 2] = 8;
					} else {
						t.Stop();//yanlış bir hareket olursa game over
						Console.Write("Game over..");
					}
					break;
				default:
					break;
			}
		}
		private static void hareket(object o, ElapsedEventArgs a)
		{
			zaman++;
			yem();//yemi kontrol et
			yazdır();//önce bir haritayı yazdır
			Console.WriteLine(zaman);//zamanı yazdır
			char b;//klavyeden yön bilgisi için bir değişken lazımdır
			if(zamani!=zaman){
				zamani=zaman;//zamanı yeni değere set et
				for(int j=0;j<uzunluk-1;j++){//her timer tickinde hareket et
					yılan[j,0]=yılan[j+1,0];
					yılan[j,1]=yılan[j+1,1];
					yılan[j,2]=yılan[j+1,2];
				}
				sürün(yılan[uzunluk-1,2]);//yön bilgisi gelmdiyse halihazırdaki yönde devam et
				yılan[0,2]=0;//kuruğu yok et
			}
			b=Console.ReadKey(true).KeyChar;//klavye oku
			yılan[uzunluk-1,2]=(int)Char.GetNumericValue(b);//char to int
			
			
			
		}
		public static void yazdır()//haritayı bastır
		{
			Console.Clear();
			//haritayı yazdır
			for (int i = 0; i < boy; i++)
			{
				for (int j = 0; j < en; j++)
				{
					for(int k=0;k<uzunluk;k++)
					{
						if(i==yılan[k,0] && j==yılan[k,1]){
							switch (yılan[k, 2]) {
								case 2:
									harita[i, j] = 2;
									break;
								case 4:
									harita[i, j] = 4;
									break;
								case 6:
									harita[i, j] = 6;
									break;
								case 8:
									harita[i, j] = 8;
									break;
								default:
									harita[i, j] = 0;
									break;
							}
						}
					}
				}
				
			}
			
			for (int i = 0; i < boy; i++)
			{
				for (int j = 0; j < en; j++)
				{
					switch (harita[i, j])
					{
						case 0:
							Console.Write(" ");
							break;
						case 2:
							Console.Write("v");
							break;
						case 3:
							Console.Write("+");
							break;
						case 4:
							Console.Write("<");
							break;
						case 6:
							Console.Write(">");
							break;
						case 8:
							Console.Write("^");
							break;
						case 5:
							Console.Write("o");
							break;
						case 9:
							Console.Write("@");
							break;
					}
				}
				Console.WriteLine(" ");
			}
		}
		public static void Main(string[] args)
		{
			/*
			*YILAN OYUNU
			* oyun  aşağı,4 sola,6 sağa 8 yukarı yem tanımlanır
*  yem yediğinde uzar duvara yada kendine çarparsa oyun biter
			 * */ 
			for (int i = 0; i < boy; i++)
			{
				for (int k = 0; k < en; k++)
				{
					
					harita[i, k] = 0;
					if ((((k == 0) || (i == 0)) || (i == boy-1)) || (k == en-1))
					{
						harita[i, k] =3;//duvarlar ör
					}
					
				}
			}
			ilk();//oyunu ilkle ve yılanı koy
			t.Elapsed += new ElapsedEventHandler(Program.hareket);
			t.Start();
			while (true)
			{
			//döngüde devam et	
			}
		}
	}
}