Курс: Разработчик игр на Unity с нуля <br>
Модуль: 6 <br>
<br>
Цель игры взломть замок подобрав комбинацию для 3х пинов используя 3 инструмента за 60 секунд.<br>
В игре 2 сложности, на простом значения инструментов для взлома статичны и не изменяются от раунда к раунду.<br>
На сложном уровне значения инструментов генерируются случайно при старте и рандомизируюся после каждого применения любого из инструментов. Цель сложного уровня не дать игроку рассчитать в каком порядке применять инструменты заранее. Каждый иснтрумент имеет свою точность, т.е. для отмычки случайные знчения будут в диапозоне от -1 до +1, для дрели от -2 до +2 и для молотка от -3 до +3.<br>
<br>

<b>Описание методов:</b> <br>
Когда канвас с игрой становится актиыным <b>OnEnable</b> случайно генерируются текущее положение пинов и положении при котором замок откроется.<br>
Так же вызываются функции обновления позиции пинов и генерация статов для инструментов.<br>
<br>

В метода <b>Update</b> с помощью <b>Time.deltaTime</b> я отнимаю от счётчика по 1 секунде. Если оствшееся время достигает значение 0 вызывается функция окончания игры. Так же если текущее положение пинов соответствует победной комбинации вызывается функция остановки игры.<br>
<br>

Метод <b>UpdatePinPositions</b> обновляет текстовое отображение текущих позиций для каждого пина, если выбран сложный уровень, то вызываю функцию для генерации случайных значений для инструментов в ней же обновляю текстовые поля в которых отображаются характеиристики инструментов.
<br>

Метод <b>updateInstDesc</b> устанавливает в текстовые поля инструментов их значения если выбран простой уровень сложности. Здесь я используя цикл для каждого инструмента добавляю пробел и знак + если число положительное исключительно для более красивого отображения текста.
<br>

Далее следуют методы <b>%инструмент%ОнКлик</b>. В них зависимости от уровня сложности я применяю тот или иной инструмент со стачными или динамческими свойствами предварительно вызвав функцию проверки не выхожу ли я за границы диапозна какого-либо из пинов. В конце расчёта новых позиций вызываю метод для обновления текстовых полей.<br>
<br>

Метод <b>CheckIfInstApllicable</b> в качестве парамерта принимает характеристики инструмента и проводит расчёт не вызовет ли его применение выход за пределы даипозона какого-либо из пинов.
<br>

Метод <b>RandomizeInstrument</b> в качестве парамерта принимает строку с название инструменита, далее я генерирую новые характеристики для инструментов (вызывается только если выбран сложный уровень), затираю строку с характеристиками инструментов и переписываю в цикле новыми значениями.
<br>

Метод <b>StopGame</b> в качестве параметра принимает булеву переменную, если таймер закончился данная функция будет вызвана с параметром false, если игрок собрал правильную комбинацию - true. Заношу в текстовое поле сообщение о победе или поражении, выключю канвас с игрой и активирую канвас с сообщением, кнопками рестарт и в меню.
