   M S B A M L   `   `   `  ����I  EYetAnotherNote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:YetAnotherNoteYetAnotherNote  5   	       YetAnotherNote.Editor   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:YetAnotherNote     5   	   -   5          � Title$  Editor��5   	   $	��500��6   $	��500��6%    � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��5
   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   $	��100��5   	   $	��100��5   	   
������ .��5      � -   5        $��
EditorGrid��5       � RowDefinitions 5   
   �� 5      -   5         �Name$ EditorTitleBarRow��5      $��20 �66   �� 5      .+�5   
   �� -   5        $��EditorTitleBar��5      $��Stretch=�6*   $��0��6F   ���	#FFC309096d   �� 5   
   -   5        $��EditorBorder��5      $��1��6%   ���	#FFC30909$��2px,0,2px,2pxq�6J   �� 5   
   -   5        $��EditorCloseButton��5      $��0��6*   
���   �$��1q�6K    �� CornerRadius$ 2��6_   $
��16px��6p   $
��16px��6}   $	��2pxq�6�   $��Right=�6�   $��Center=�6�   ._�5      �� -   5        $	��✖��5      $��12(�6!   
�������$��Center=�6B   $��Center=�6_   
���   �6�   5      �� 5   
   -   5        $��
MainCanvas��5      $��1��6   $��2,-1,2,2q�62   .+�5      �� -	   5        $��EditorTextBox��5       �� TextWrapping$
 Wrap=�6G    �� AcceptsReturn$
 True��6�   
���   �
�������
������ ��6[   �� *ActualHeight+	 �� ElementName$	 
MainCanvas����6�   �� *ActualWidth+$	 
MainCanvas��
 � ContextMenu
 6G  Z� 5      5!      5"      