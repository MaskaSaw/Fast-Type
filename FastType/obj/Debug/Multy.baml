   M S B A M L   `   `   `  ����C  ?FastType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:FastTypeFastType  5   	       FastType.Multy   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:FastType     5   	   -   5          � Title$  Single��5   	   ���#0E100E$-��'/FastType;component/Fonts/#Luckiest Guy)�5
   	   $	��768��5   	   $
��1366��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � WindowState$ 	Maximized=�5   	   $��30(�5   	    � ShowInTaskbar .   � AllowsTransparency . .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��5* �6   �� 5      $��2* �6   .+�5    
   �� -   5         ��Name$ LoginBox��5       $��0��5!      $��Center=�5"      $
��Left=�5#      ���	#FF32af0a$��45(�5%      	 �� TextAlignment$	 Center=�5&      $	��350��5'      $��20 0 0 0q�5(      � 5)   
   -   5        
 �Name$
 Choice��5)      $��90��5*      $	��300��5+      $��Right=�5,       5-      �� 5.      �� 5/      �� 50      52      �� 54   
   -   5         ��Name$ bBack��54      $��7��55      $��
Назад��56      $��Center=�57      $��Right=�58      ���	#FF32af0a���#0E100E$	��170��5;      $��50��5<      $��35(�5=      ���	#FF32af0a$��
0 20 40 20q�5?      6%   �� 5A   
   -   5        $ bStart��5A      $��2��5B      $��
Старт��5C      $��Center=�5D      $��Center=�5E      $��Center=�5F      ���	#FF32af0a���#0E100E$	��180��5I      $��60��5J      $��45(�5K      ���	#FF32af0a$��10,10,1146,100q�5M      6)   �� 5O   
   -   5        $ bStop��5O      $��2��5P      $��Стоп��5Q      $��Center=�5R      $��Center=�5S      ���	#FF32af0a���#0E100E$	��160��5V      $��60��5W      $��45(�5X      ���	#FF32af0a$��10,80,1146,0q�5Z      6'   �� 5\   
   -   5        $ Player1Board��5\      $��Игрок1��5]      $��2��5^      $��Center=�5_      ���	#FF32af0a$��45(�5a      $	 Center=�5b      $	��250��5c      $��253,22,863,114q�5d      �� 5f   
   -   5        $ Player2Board��5f      $��Игрок2��5g      $��2��5h      $��Center=�5i      ���	#FF32af0a$��45(�5k      $	 Center=�5l      $	��250��5m      $��253,106,863,30q�5n      �� 5p   
   -	   5        $ ScoreCountBox1��5p      $��
Очки: ��5q      $��2��5r      $��Center=�5s      ���	#FF32af0a$��45(�5u      $	 Center=�5v      $	��130��5w      $��0,0,200,100q�5x      �� 5|   
   -
   5        $ 	ScoreBox1��5|      $��0��5}      $��2��5~      $��Center=�5      ���	#FF32af0a$��45(�5�      $
	 Left=�5�      $	��120��5�      $��
50,0,0,100q�5�      �� 5�   
   -   5        $ ComboCountBox1��5�      $��Комбо: ��5�      $��2��5�      $��Center=�5�      $��Center=�5�      $��400 0 0 100q�5�      ���	#FF32af0a$��45(�5�      $	 Center=�5�      $	��170��5�      �� 5�   
   -   5        $ 	ComboBox1��5�      $��0��5�      $��2��5�      $��Center=�5�      $��Center=�5�      $��700 0 0 100q�5�      ���	#FF32af0a$��45(�5�      $
	 Left=�5�      $	��120��5�      �� 5�   
   -   5        $ ScoreCountBox2��5�      $��
Очки: ��5�      $��2��5�      $��Center=�5�      ���	#FF32af0a$��45(�5�      $	 Center=�5�      $	��130��5�      $��
0,80,200,0q�5�      �� 5�   
   -   5        $ 	ScoreBox2��5�      $��0��5�      $��2��5�      $��Center=�5�      ���	#FF32af0a$��45(�5�      $
	 Left=�5�      $	��120��5�      $��648,106,598,30q�5�      �� 5�   
   -   5        $ ComboCountBox2��5�      $��Комбо: ��5�      $��2��5�      $��Center=�5�      $��Center=�5�      $��798,106,398,30q�5�      ���	#FF32af0a$��45(�5�      $	 Center=�5�      $	��170��5�      �� 5�   
   -   5        $ 	ComboBox2��5�      $��0��5�      $��2��5�      $��Center=�5�      $��Center=�5�      $��973,106,273,30q�5�      ���	#FF32af0a$��45(�5�      $
	 Left=�5�      $	��120��5�      �� 5�   
   -   5         ��Name$ 
PlayerImg1��5�      $��1��5�      $	��110��5�      $	��110��5�      $
��Left=�5�      $��Bottom=�5�      $��	40 0 0 80q�5�      $��Images\Player\1.png��5�      �� 5�   
   -   5        $ 
PlayerImg2��5�      $��1��5�      $	��110��5�      $	��110��5�      $
��Left=�5�      $��Bottom=�5�      $��
140 0 0 80q�5�      $��Images\Player\2.png��5�      �� 5�   
   -   5        $ 	LetterBox��5�      $�� ��5�      $��1��5�      $��Center=�5�      $	��Top=�5�      ���	#FF32af0a���#0E100E$	��115(�5�      $	 Center=�5�      $	��150��5�      $	��150��5�      $��
0 20 200 0q�5�      �� 5�   
   -   5        $ TimeBox��5�      $�� ��5�      $��1��5�      $��Center=�5�      $	��Top=�5�      ���	#FF32af0a���#0E100E$	��115(�5�      $	 Center=�5�      $	��150��5�      $	��150��5�      $��200 20 0 0 q�5�      �� 5�   
   -   5        $ EnemyImg��5�      $��1��5�      $	��110��5�      $	��110��5�      $��Right=�5�      $��Bottom=�5�      $��	0 0 0 350q�5      $��Images\Enemy\2.png��5     $y�Hidden=�5     �� 5  
   ���	#FF32af0a$��1��5     $
��1366��5     $��10��5     $��Center=�5	     $��Bottom=�5
     $��0 0 0 60q�5     5     5     