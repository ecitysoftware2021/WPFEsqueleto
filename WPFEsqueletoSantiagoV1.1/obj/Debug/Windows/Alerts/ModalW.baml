   M S B A M L   `   `   `  ����S  OWPFEsqueletoSantiagoV1.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:WPFEsqueletoSantiagoV1._1.Windows!WPFEsqueletoSantiagoV1._1.Windows  5   	   .    (WPFEsqueletoSantiagoV1._1.Windows.ModalW   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:WPFEsqueletoSantiagoV1._1.Windows     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   "  WpfAnimatedGif.ImageBehavior    AnimateInDesignMode  .  � Title$ ModalWindow��5
   	    � WindowStyle$
 None=�5   	    � Topmost .   � WindowState$ 	Maximized=�5   	   $��2064.095��5   	   $��1250.599��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $	��360 �6    �� 5      $��* �6    �� 5      $	��400 �6    .+�5      �� $��1��6   $��10,10,10,10q�6"   ���	#FF68B549 �� CornerRadius$ 30,30,30,30��6Y   $��50,0,50,200q�6t   
�������._�5      � .+�5      �� -   5         ��Name$ 	LblTittle��5       $��Center=�5       $��0,80,0,0q�5!      $��55(�5"      $��
Myriad Pro)�5#      $
��Bold"�5$      $	��Top=�5%      	 �� TextAlignment$	 Center=�5&      ��5      �� *Tittle+5&      �� 5(      -   5        $ 
LblMessage��5(       $��Center=�5*      $��	0,0,0,200q�5+      $��48(�5,      $	��700��5-      $��
Myriad Pro)�5.      $��WrapWithOverflow=�5/      $��Center=�50      $	 Center=�51      ��5)      �� *	Messaje+51      �� 53      -   5        $ LblMessageTouch��53       $(��"¡Toca la pantalla para continuar!��54      $��Center=�55      $��0,0,0,30q�56      $��30(�57      $	��500��58      $��
Myriad Pro)�59      $��WrapWithOverflow=�5:      $��Bottom=�5;      $	 Center=�5<      �� 5=      -   5        
 ��Name$
 
GifLoadder��5@         AnimatedSource$ /Images/Gif/loading.gif��5=        .    	AutoStart . $��Uniform=�5A      $��Bottom=�5B      $��Center=�5C      $	��300��5D      $��0,0,0,20q�5E      $y�Hidden=�5F      �� 5H      -   5        $
 BtnOk��5H      $��Bottom=�5I      $��Center=�5J      $	��100��5K      $��0,0,0,50q�5L      $��/Images/others/x.png��5N      $��Uniform=�5O      �� 5Q      -   5        $
 BtnNo��5Q      $��Bottom=�5R      $
��Left=�5S      $	��100��5T      $��
100,0,0,50q�5U      $��/Images/others/x.png��5W      $��Uniform=�5X      �� 5Z      -   5        $
 BtnYes��5Z      $��Bottom=�5[      $��Right=�5\      $	��100��5]      $��
0,0,100,50q�5^      $��/Images/others/x.png��5`      $��Uniform=�5a      5b       J� Effect 5c      3  -System.Windows.Media.Effects.DropShadowEffect  5d         ShadowDepth$ 20Q�5e         	Direction$	 225Q�5f         Color$
 Gray��5g         Opacity$	 0.6Q�5h         
BlurRadius$
 30.0Q�5i      5k      5l      5m      5n      