   M S B A M L   `   `   `  ���� I  EYetAnotherNote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:YetAnotherNoteYetAnotherNote  !    YetAnotherNote.ImportExport   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:YetAnotherNote       � Title$  ImportExport��$	��450��$	��800�� � WindowStyle$
 None=� � AllowsTransparency$
 True�� � 
ResizeMode$ NoResize=�$	��100��$	��100��
������ .���  � RowDefinitions �� -    �Name$ EditorTitleBarRow��$��20 ��� .+��� -   $��EditorTitleBar��$��Stretch=�$��0�����	#FFC30909�� -   $��EditorBorder��$��1�����	#FFC30909$��2px,0,2px,2pxq��� -   $��EditorCloseButton��$��0��
���   �$��1q� �� CornerRadius$ 2��$
��16px��$
��16px��$	��2pxq�$��Right=�$��Center=�._��� -   $	��✖��$��12(�
�������$��Center=�$��Center=�
���   ��� -   $��ScrollViewer��$��1��$��2,-1,2,2q�.���� -   $��ImportExportTextBox�� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=�	 �� AcceptsReturn$
	 True��
���   �
�������
������ 
 � ContextMenu
 Z� �� -   $��
SaveButton��$��1��$	��100��$��30��$
��Save�����	#FFC30909
���   ����	#FFC30909$��3q�$��16(�$y�Visible=�$��30q�$��Bottom=�$��Right=�