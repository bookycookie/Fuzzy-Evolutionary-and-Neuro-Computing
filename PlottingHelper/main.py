import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
import pandas as pd
from sympy.physics.quantum.circuitplot import matplotlib

colors = [u'#1f77b4', u'#ff7f0e', u'#2ca02c', 'black']
x_2x8x3 = np.array(
    [0.8690135125278919, 0.12638708735770643, 0.6221282539151535, 0.36972379116871884, 0.8666955196367061,
     0.13210552369926148, 0.3765355203923565, 0.6287633375333634])
y_2x8x3 = np.array([0.25784953583404113, 0.7424153560965489, 0.7393431022111406, 0.7451495793873972, 0.7358483798384345,
                    0.26703046043581735, 0.2628305890974326, 0.25918784901365577])

s_x_2x8x3 = np.array(
    [0.07633476766478509, 0.08330183121042864, 0.138984463043824, 0.10540445265166581, 0.1148920144391248,
     0.11487602134520815, 0.13197334174574105, 0.1106055232407753])

s_y_2x8x3 = np.array(
    [0.15906436313251043, 0.1907268506405052, 0.26397203634121083, 0.22888148925288924, 0.242431140645384,
     0.23222247076613878, 0.2484477760661019, 0.20361869618284256])

x_2x8x4x3 = np.array(
    [0.8416089378604239, 0.13038412917706327, 0.36308883994976954, 0.11644110858970805, 0.8312756927457832,
     0.07074957519504704, 0.6208131816958715, 0.37927475903203894])

y_2x8x4x3 = np.array(
    [0.6574402556353958, 0.32529294072553183, 0.7569466304239587, 0.21693544060953174, 0.7757260286178507,
     0.8703508184088043, 0.7495024783080437, 0.2476952346173511])

s_x_2x8x4x3 = np.array(
    [0.4535616516937157, 0.2342623808176424, 0.13554429706814064, 4.2899604708611, 0.6786469301699731,
     7.638954078226123, 0.11886505775675349, 0.09187436482850933])

s_y_2x8x4x3 = np.array(
    [0.14911093930038927, 0.12248107698762914, 0.6186562693368411, 0.25583792375678693, 0.18393605855696055,
     0.1327771584424322, 10.065802092058352, 0.21134892665152952])

x_2x6x4x3 = np.array(
    [0.6902336767749842, 0.4346749344871269, 0.34021973043148296, 0.1319377261238857, 0.6372057838083995,
     0.3756314603826758, 0.6612880368995122, 1.8298511274541993])

y_2x6x4x3 = np.array(
    [0.7437977303139974, 0.7464743043859277, 0.7508430198265397, 0.6991351280522484, 0.2542084682634458,
     0.26679288603475004, -2.111392035066565, 2.941535007526741])

s_x_2x6x4x3 = np.array(
    [1.5008380295776693, 0.08194816364385646, 0.0750035385342541, 0.4824141036763956, 0.10003848183281033,
     0.22502035807076012, 2.8101011160504417, 7.233901753386353])

s_y_2x6x4x3 = np.array(
    [7.151519134106867, 2.616694704153685, 0.6858204328330808, 0.20097207501330075, 0.16194741949083621,
     0.18019677552673707, 5.478744536563643, 2.1985397123808044])


def plot_task():
    def y(x, w, s):
        return 1 / (1 + abs(x - w) / abs(s))

    x_range = np.linspace(start=-8, stop=10, num=100)

    for i, s in enumerate([0.25, 1, 4]):
        plt.plot(x_range, [y(x, 2, s) for x in x_range], c=colors[i], label=f's={s}, w=2')

    plt.ylim([0, 1])
    plt.xlabel('x')
    plt.ylabel('y')
    plt.legend()
    plt.show()


def test_plot(x_param, y_param):
    chunk = np.loadtxt(r'C:/git/Fuzzy-Evolutionary-and-Neuro-Computing/Homework_7/Files/Dataset/zad7-dataset.txt')
    data = np.array(chunk)
    x = data[:, 0]
    y = data[:, 1]

    label = [0 if a == 1 else 1 if b == 1 else 2 for a, b, c in zip(data[:, 2], data[:, 3], data[:, 4])]

    x = np.append(x, x_param, axis=0)
    y = np.append(y, y_param, axis=0)
    label = np.append(label, [3 for i in range(len(x_param))])
    plt.scatter(x, y, c=label, cmap=matplotlib.colors.ListedColormap(colors))
    plt.show()


def plot(path):
    data = pd.read_csv(path, sep='\t', names=["X", "Y", "A", "B", "C"])
    data["Category"] = ["A" if a == 1 else "B" if b == 1 else "C" if c == 1 else "D" for a, b, c in
                        zip(data["A"], data["B"], data["C"])]
    sns.scatterplot(data=data, x="X", y="Y", s=50, hue="Category", style="Category")
    plt.show()


def plot_scale(s_x, s_y):
    plt.plot(s_x, label="x")
    plt.plot(s_y, label="y")
    plt.xlabel("Neuron")
    plt.ylabel("Variance magnitude")
    plt.legend()
    plt.show()


def main():
    root = "C:/git/Fuzzy-Evolutionary-and-Neuro-Computing/Homework_7/Files/"
    test = root + 'Task1/s4.txt'
    test2 = root + 'Dataset/zad7-dataset.txt'
    test3 = root + 'Parameters/Data/2x8x3test.txt'

    # plot_task()
    # plot(test2)
    # test_plot(x_2x8x3, y_2x8x3)
    # test_plot(x_2x8x4x3, y_2x8x4x3)
    test_plot(x_2x6x4x3, y_2x6x4x3)
    # plot_scale(s_x_2x8x3, s_y_2x8x3)
    # plot_scale(s_x_2x8x4x3, s_y_2x8x4x3)
    plot_scale(s_x_2x6x4x3, s_y_2x6x4x3)


if __name__ == '__main__':
    main()
