   M S B A M L   `   `   `  ����N  JInternet Speed Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Internet_Speed_TestInternet_Speed_Test  5   	   $    Internet_Speed_Test.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Internet_Speed_Test     5   	     � WindowStyle$
  None=�5   	   
������  �� BitmapScalingMode$ HighQuality=�5
   	    � AllowsTransparency .  � WindowState$ 	Maximized=�5   	    � Title$ 
MainWindow��5   	   $
��1080��6   $
��1920��6*    � 	Resources 5         settings_button  settings_button_click  SettingsMenu ��Name �� EdgeMode  FontMenu ��Name	 J� RenderTransformOrigin
 ��Name  Information  TransparentStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%�	  &	        &	 4     &	 i     &	 E    &	 a    &	 g    �� 5   
   $ ��Images/settings_button.png��6(   �� 5   
   $!��settings_button_clicked.png��6.   �� 5   
   $
��Left=�6*   $��207,110,0,0q�6E   $	��Top=�6Z   $	��150��6r   .+�5      �� $ ChangeFontButton��6   -   5        $��Change Font��5   /   ���	#FF332288���	#FFDDCC77
���   �$	��2,1q�6�   $
��Bold"�6�   $��16(�6�   $ Aliased=�6�   6�   �� 5      $ 
MoveButton��6   -   5        $
��Move��5   )   ���	#FF332288���	#FFDDCC77
���   �$	��2,1q�6z   $
��Bold"�6�   $��16(�6�   $ Aliased=�6�   6�   5      � 5   
   $
��Left=�6    $	��100��6;   $��
142,10,0,0q�6H   $	��Top=�6\   $	��300��6t   ���	#FF332288.+�5      �� 
���   �$��2q�6)   �� 5      $��20(�6   ���	#FFDDCC77$��Color��69   �� 5      $��20(�6   ���	#FFDDCC77$��R:��69   $��
55,0,-59,0q�6F   �� 5      $ InputR��6e   $��50��6   $��20��6!   $��78,10,172,70q�6-   $	 	2.22,-0.5&�6C   6e   �� 5      $
 InputG��6\   $��20(�6   ���	#FFDDCC77$��G:��69   $��130,0,-141,0q�6F   6\   �� 5      $��50��6   $��20��6!   $��156,10,94,70q�6-   �� 5      $
 InputB��6\   $��20(�6   ���	#FFDDCC77$��B:��69   $��205,0,-221,0q�6F   6\   �� 5      $��50��6   $��20��6!   $��232,10,18,70q�6-   �� 5      -   5        $��Change Font��5      $��143,36,53,34q�6+   $��16(�6A   6O   �� 5      $
 
ErrorLabel��6G   $��14(�6   
���  ��$��
8,66,-8,-1q�63   6G   �� 5       $
��Left=�6   $��	10,40,0,0q�62   $	��Top=�6E   $	��120��6]   $	 0.471,-4.091&�6i   5!      � 5"   
   $
��Left=�6#   $	��100��6>   $��120,235,0,0q�6K   $	��Top=�6`   $	��300��6x   ���	#FF332288.+�5#      �� 
���   �$��2q�6)   �� 5$      ���	#FFDDCC77$��12(�6+   .��5%      �� $��90��6   �� 6)   $6�0This program shows ping an download speed. Right��6-   �� 6g   �� 6s   $5�/click a component to customize it. Left click a��6w   �� 6�   �� 6�   $.�(component to make the customization menu��6�   �� 6�   �� 6�   $�
disappear.��6  6  5&      5'      �� 5(   
   " ��6)   .�5)      ��  � �� 5*      �� 5+      " ��6&   .R�5,      �� ��5-      �� 5.   "   " ��6(    5/   &   P� 50   *    �@J�IsMouseOver . . �51   .   ��  � ��
 ���� 6K   52   +   54   #   ._�56      � 
������ .+�57   "   �� 65   58      59      5:      5<      5=      5>      .��5?      � -   5         �Name$
 Grid��5?      $��0,2,0,-2q�6   .+�5@   
   �� -   5         ��Name$ PingComponent��5@      $
��Left=�5A      $	��100��5B      $	��Top=�5C      $	��100��5D      $%��Images/component_background.png��5E      $��	10,10,0,0q�5F      5H      �� 5I   
   -   5        $
 PingText��5I      $��Loading��6"   $
��Left=�64   $��	15,40,0,0q�6O   $	��Top=�6b   $��37��6z   ���	#FFDDCC77$
��Bold"�6�   $��20(�6�   �� 5J   
   -   5        $ DownloadComponent��5J      $
��Left=�5K      $	��100��5L      $	��Top=�5M      $	��100��5N      $%��Images/component_background.png��5O      $��
10,120,0,0q�5P      5R      �� 5S   
   -   5        $
 DownloadText��5S      $��Loading��6&   $
��Left=�68   $��
15,150,0,0q�6S   $	��Top=�6g   $��37��6   ���	#FFDDCC77$
��Bold"�6�   $��17(�6�   �� 5T   
   -	   5        $ MoreInfoButton��5U      $
��Left=�5V      $��
10,230,0,0q�5W      $	��Top=�6!   $	��105��69   $	��105��5X      ���	#00DDDDDD$��0q�5Z      #��[ 5\      .��5]      �� -
   5        $ MoreInfoImage��5]      $	��100��6+   $	��100��67   $#��Images/information_button.png��6D   5^      5_      5`      