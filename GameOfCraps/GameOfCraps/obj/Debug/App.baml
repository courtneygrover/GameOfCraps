   M S B A M L   `   `   `  ����F  BGameOfCraps, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GameOfCrapsGameOfCraps  5          GameOfCraps.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      +localclr-namespace:GameOfCraps     5        �� 	Resources  5         ButtonImage �� ImageSource  DisabledButtonImage  WindowImage �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   
BlurRadius   Color �� 
TargetType	 J� SnapsToDevicePixels
 �� Triggers P� Property P� Value   Opacity��%  &	        &	 5     &	 r     '� �     7  '�� �     '�� |    '�� 6    7   7  �� 5   
   $ Resources/goldButt.png��6)   6P   �� 5   
   $$ Resources/disabledGoldButt.png��61   6`   �� 5	   
   $ Resources/greenFelt.jpg��6)   6Q   �� 5   
   " �6   .�5      ��  � ��8   6+   5      �� 5   
   " ��6   .�5      ��  �@J�Effect 5        5      $ 0Q�6'   $ 1Q�67   $ Black��6F   5      5      �� 5   
   " ��6   .�5      ��  � ��
 � ���6+   �� 5       �@J�Effect 5        5      $ 0Q�6'   $ 1Q�67   $ 	#FF000000��6F   5      5      �� 5    
   " ��6   .�5!      ��  � ��8   6+   �� 5"       � �� 5#      �� 5$      " ��6&   .R�5%      �� #��z��6!   #��z��6K   $��0q�6w   ._�5&      �� #��z��6/   #��z��5'   !   #��z��6D   #	 z	 5(   !   ��. 6]   5)      
 5+      P� 5-       � {� .  . �5.   "   �� 8   � ��6?   5/      P� 51       �@J�IsMouseOver . . �52   "   ��  � ��
 �  ��6?   �� 53   "    �@�Cursor$
 Hand��6;   �� 54   "    �@J�Effect 55   &     56   *   $ Black��6;   $	 0.5Q�6I   $ 0Q�6W   $ 17Q�6g   58   #   59      5=      5?      5@      5A      5B      