package utils;

import java.util.Random;

public class EnhancedRandom {
    private static Random random = new Random();

    public static double nextDouble(double start, double end){
        double randomValue = start + (end - start) * random.nextDouble();
        return randomValue;
    }

    public static int nextInt(int start, int end){
        int randomValue = start + (end - start) * random.nextInt();
        return randomValue;
    }

    public static long nextLong(long start, long end){
        long randomValue = start + (end - start) * random.nextLong();
        return randomValue;
    }
}
