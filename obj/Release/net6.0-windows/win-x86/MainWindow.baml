   M S B A M L   `   `   `  ���� R  NColorPicker, Version=3.4.1.0, Culture=neutral, PublicKeyToken=1c61eec504ce2276>.clr-namespace:ColorPicker;assembly=ColorPickerColorPicker  I EYetAnotherNote, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:YetAnotherNoteYetAnotherNote    YetAnotherNote.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Fcolorpicker.clr-namespace:ColorPicker;assembly=ColorPicker     .localclr-namespace:YetAnotherNote     $	��450��$	��600��  � AllowsTransparency$
  True�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�.��� 
���   � � RowDefinitions �� $
��20px ��� $
��80px ��� $��* �.+��� -   $��1��
���  ��$��1q� �� CornerRadius$ 2��$��40��$��40��$
��20,0q�$
��Left=�
���  ��$��Center=�._��� $
��📁��$��-4,0,0,0q�$��35(� �� 
LineHeight$ 40�� �� TextAlignment$ Right=� �� LineStackingStrategy$ BlockLineHeight=�
���   � � ToolTip _� $��25�����#353535$��0q����#f7c7c3.���� $��Center=�$	��Top=�	 �� Padding$	 3q����#f7c7c3$ 	MaxHeight=�$-��'Create new Folder in the deafult folder���� -   $��1�����	#FFC30909$��1q�$ 2��$��40��$��40��$
��80,0q�$
��Left=�
���  ��$��Center=�._��� $
��🗎��$��4,3,5,0q�$
��Bold"�$��45(�$ 40��$ Right=�$ BlockLineHeight=�
���   � _� $��25�����#353535$��0q����#f7c7c3.���� $��Center=�$	��Top=�$	 3q����#f7c7c3$ 	MaxHeight=�$-��'Create new Preset in the deafult folder���� -   $��1�����	#FFC30909$��1q�$ 2��$��40��$��40��$��140,0q�$
��Left=�
���  ��$��Center=�._��� $	��ⓘ��$��-1,2,0,0q�$
��Bold"�$��45(�$ 40��$ Right=�$ BlockLineHeight=�
���   � _� $��25�����#353535$��0q����#f7c7c3.���� $��Center=�$	��Top=�$	 3q����#f7c7c3$ 	MaxHeight=�$��Import Preset���� � -   $��PresetsGrid��$��2��
 � ColumnDefinitions
 �� $��100px��$��260px��$��180px ��� $	��3px ��� -    �Name$ 
PresetInfo��$��* �.+��� $
��Auto=�.���� -   $��ScrollViewerContent��$��0,0,0,3q��� $��1��$	��5px��$��Stretch=����	#FFC30909� -   $��PresetControls��$��2��$y�Hidden=� �� �� 
 �� $	��120 ��� $��* �.+�d� -   $��
ShowButton��$��1��$��0��$��60��$��30��$��Right=�$	��Top=����	#FFC30909
���   �$
��Show�����	#FFC30909$��3q��� $��0��$��2��$��
Foreground��
�������$��30��$	��Top=�$��Center=�$	��105��$��20(�   ColorPicker.SquarePicker  -	   $��FColorSquare��$��0��$��0��$	��Top=�$��10q�
������ #   ColorPicker.PickerControlBase   
ColorState ��  �� ElementName$ maindisplay�� �� Path$ 
ColorState� �� Mode$ TwoWay=� �� Delay$ 10���� $��#��$��0��$��0��$��Bottom=�$��10q�
��������� -
   $��	FColorHex�� �� CharacterCasing$ Upper=�$��FFFFFF��$��0��$��0��$��Bottom=�$��20,10q�   ColorPicker.AlphaSlider  -   $��FColorAlpha��$��0��$��0��$��Bottom=�$��10,40q� �� $ maindisplay��$ 
ColorState�$ TwoWay=�$ 10��� $��0��$��1��$	��220��$��25��
 �� $��80 ��� $	��120 ��� $��20 � �� �� .+��� $��0��$��2��$��Font Size: ��
�������$��15(�$��80��$��Center=��� -   $��FontSizeTextBox��$��16(�$��1��$��12��$��2��$��Stretch=�$��Center=�$��25���� -   $��2��$��0��$��20��$	��▲��$��2,-3,0,2q�$��11(�$��Right=��� -   $��2��$��1��$��20��$	��▼��$��2,-2,0,2q�$��11(�$��Right=��� $��1��$��2��$��
Background��
�������$��30��$	��Top=�$��Center=�$	��105��$��20(�  -   $��BColorSquare��$��1��$��0��$	��Top=�$��10q�
������  �� $ maindisplay��$ 
ColorState�$ TwoWay=�$ 10���� $��#��$��1��$��0��$��Bottom=�$��10q�
��������� -   $��	BColorHex��$ Upper=�$��000000��$��1��$��0��$��Bottom=�$��20,10q�  -   $��BColorAlpha��$��1��$��0��$��Bottom=�$��10,40q� �� $ maindisplay��$ 
ColorState�$ TwoWay=�$ 10���� $��1��$��1��$
��Left=�$	��Top=�$��20,40q�.+��� -   $��CBClickThrough��$��0,10,0,0q� � LayoutTransform ��  �� ScaleX$	 1.3Q� �� ScaleY$	 1.3Q�.���� $��Enable Click through��
��������� -   $��	CBTopMost��$��0,10,0,0q� �� $	 1.3Q�$	 1.3Q�.���� $��Always on top��
��������� -   $��CBRemoveBorder��$��0,10,0,0q� �� $	 1.3Q�$	 1.3Q�.���� $��Remove Border��
��������� ���	#FFC30909$��0��$��2��$��	0,0,0,1.5q��� ���	#FFC30909$��1��$��2��$��	0,1.5,0,0q��� $��1�����	#FFC30909$��3px,0,3px,1.5pxq��� -   $��TitleBar��$
��20px��$	��Top=����	#FFC30909�� $��0��
���   �$��1q�$ 2��$
��16px��$
��16px��$	��2pxq�$��Right=�$��Center=�._��� -   $��MainWindowCloseButton��$	��✖��$��12(�
�������$��Center=�$��Center=�
���   ��� $��0��
���   �$��1q�$ 2��$
��16px��$
��16px��$��20px,2pxq�$��Right=�$��Center=�._��� -   $��MainWindowMinimizeButton��$
��🗕��$
��16px��$��15(�$
 16px��$ BlockLineHeight=�$
��16px��
�������
���   �$��Center=�$��Center=��� $��2�����	#FFC30909$��3px,1.5px,3px,3pxq� � 	Resources  �� MergedDictionaries �� Source��  �� $ ScrollBar.xaml?�%    