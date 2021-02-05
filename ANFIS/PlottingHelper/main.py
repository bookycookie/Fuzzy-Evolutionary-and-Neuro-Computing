import numpy as np
import matplotlib
import matplotlib.pyplot as plt


def plot(path):
    chunk = np.loadtxt(path)
    data = np.array(chunk)

    Xs = data[:, 0]
    Ys = data[:, 1]
    Zs = data[:, 2]

    fig = plt.figure()
    ax = fig.add_subplot(111, projection='3d')

    surf = ax.plot_trisurf(Xs, Ys, Zs)
    fig.tight_layout()
    plt.show()


def plotSigmoid(path):
    chunk = np.loadtxt(path)
    data = np.array(chunk)
    Xs = data[:, 0]
    Ys = data[:, 1]
    Zs = data[:, 2]

    fig = plt.figure()

    som = fig.add_subplot(111)
    # som.set_xlim([-4, 4])
    # som.set_ylim([0, 1])

    plt.plot(Xs, Ys)

    fig.tight_layout()
    plt.show()


def main():
    root = "C:/Faks/NENR/NENR6"
    test = root + '/NENR6/Data/sampled.txt'
    plot(test)
    # plotSigmoid(test)


if __name__ == '__main__':
    main()
