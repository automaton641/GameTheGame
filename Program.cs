using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Numerics;
namespace GameTheGame
{
    class Program
    {
        public static string[] lines;
        public static int sampleRate = 48000;
        public static double c = 0;
        public static double[] waveForm;
        public static double d;
        public static double f;
        public static List<double> wave = new List<double>();
        public static void Main(string[] args)
        {
            waveForm = new double[2];
            waveForm[0] = 1;
            waveForm[1] = -1;
            d = 0.333;
            f = 220;
            Loop3();
            ApplyEcho();
            Normalize();
            //printWave(wave);
            SaveWave(wave, "song.wave");
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
        public static void Loop3() {
            Loop1();
            Loop2();

            double c2 = c;
            Loop1();
            Loop2();
            c = c2;
            double f2 = f;
            f /= 3.0/2.0;
            Loop1();
            Loop2();
            f = f2;

            c2 = c;
            Loop1();
            Loop2();
            c = c2;
            f2 = f;
            f /= 4.0/3.0;
            Loop1();
            Loop2();
            f = f2;

            c2 = c;
            Loop1();
            Loop2();
            c = c2;
            f2 = f;
            f /= 5.0/4.0;
            Loop1();
            Loop2();
            f = f2;

            Loop4();
            Loop4();
            Loop4();
            Loop4();
        }
        
        public static void Loop1() {
            Put(f,d/2);
            c+=d;
            Put(f,d/2);
            c+=d;
            Put(f,d/2);
            c+=d;
            Put(f,d/2);
            c+=d;
        }
        public static void Loop2() {
            Put(f,d/2);
            c+=d/2;
            Put(f*5/4,d/2);
            c+=d/2;
            Put(f*5/4*4/3,d/2);
            c+=d/2;
            Put(f*5/4*4/3*3/2,d/2);
            c+=d/2;
            Put(f,d/2);
            c+=d/2;
            Put(f*5/4,d/2);
            c+=d/2;
            Put(f*5/4*4/3,d/2);
            c+=d/2;
            Put(f*5/4*4/3*3/2,d/2);
            c+=d/2;
        }
        public static void Loop4() {
            Put(f*2/1,d/2);
            c+=d/2;
            Put(f*3/2,d/2);
            c+=d/2;
            Put(f*4/3,d/2);
            c+=d/2;
            Put(f*5/4,d/2);
            c+=d/2;

            Put(f*2/1,d/4);
            c+=d/4;
            Put(f*3/2,d/4);
            c+=d/4;
            Put(f*4/3,d/4);
            c+=d/4;
            Put(f*5/4,d/4);
            c+=d/4;
            Put(f*2/1,d/4);
            c+=d/4;
            Put(f*3/2,d/4);
            c+=d/4;
            Put(f*4/3,d/4);
            c+=d/4;
            Put(f*5/4,d/4);
            c+=d/4;
        }

        public static void Normalize() {
            Console.WriteLine("Normalize1");
            double maximum = 0;
            double challenger = 0;
            foreach (double sample in wave) {
                if (sample < 0) {
                    challenger = sample*(-1);
                } 
                else
                {
                    challenger = sample;
                }
                if (challenger > maximum)
                {
                    maximum = challenger;
                }
            }
            Console.WriteLine("maximum: " + maximum);
            if (maximum>0)
            {
                Console.WriteLine("Normalize2");
                for(int i = 0; i < wave.Count; i++) {
                    wave[i] = wave[i]/maximum;
                }
            }
        }
        public static void PrintWaveForm() {
            for(int i =0; i < waveForm.Length; i++) {
                Console.WriteLine(waveForm[i]);
            }
        }

        public static void ApplyEcho() {
            c = 0.0;
            int echoes = 2;
            double echoTime = 1.0/3.0;
            List<List<double>> waves = new List<List<double>>(echoes);
            double scalar = 1.0*1.0/2.0;
            for(int i = 0; i < echoes; i++) {
                waves.Add(Multiply(wave, scalar));
                scalar *= 1.0/2.0;
            }
            for(int i = 0; i < echoes; i++) {
                c += echoTime;
                wave = AddWaves(wave, waves[i]);
                echoTime *= 2.0/3.0;
            }
            c = 0.0;
        }

        public static List<double> Multiply(List<double> wave, double scalar) {
            List<double> multiplied = new List<double>(wave.Count);
            for (int i = 0; i < wave.Count; i++) {
                multiplied.Add(wave[i]*scalar);
            }
            return multiplied;
        }

        public static void Put(double frequency, double time) {
            List<double> newWave = WriteWave(waveForm, frequency, time);
            wave = AddWaves(wave, newWave);
        }
        
        public static List<double> AddWaves(List<double> waveA, List<double> waveB) {
            int index = (int)Math.Ceiling(c*sampleRate);
            int resultLength = Math.Max(index+waveB.Count, waveA.Count);
            List<double> result = new List<double>(resultLength);
            int i = 0;
            for (i = 0; i < resultLength; i++) {
                result.Add(0.0);
            }
            i = 0;
            foreach (double sample in waveA) {
                result[i] += sample;
                i++;
            }
            i = index;
            foreach (double sample in waveB) {
                result[i] += sample;
                i++;
            }
            return result;
        } 

        public static List<double> WriteSilence(double time) {
            double totalSamplesReal = time*sampleRate;
            int totalSamples = (int)Math.Ceiling(totalSamplesReal);
            List<double> samples = new List<double>(totalSamples);
            for (int i = 0; i < totalSamples; i++) {
                samples.Add(0.0);
            }
            return samples;
        }
        public static List<double> WriteWave(double[] waveForm, double frequency, double time) {
            double length = (double)sampleRate / (double)waveForm.Length / frequency;
            int repetitions = (int) Math.Ceiling(time*(double)sampleRate/(double)waveForm.Length/length);
            return WriteWaveRaw(waveForm, length, repetitions);
        }

        public static void PrintWave(List<double> wave) {
            foreach (double sample in wave) {
                Console.WriteLine(sample);
            }
        }
        public static void SaveWave(List<double> wave, string fileName) {
            lines = new string[wave.Count];
            int i = 0;
            foreach (double sample in wave) {
                lines[i] = sample.ToString();
                i++;
            }
            File.WriteAllLines(fileName, lines);
        }
        public static List<double> WriteWaveRaw(double[] waveForm, double length, int repetitions) {
            
            double totalSamplesReal = waveForm.Length*length*repetitions;
            int totalSamples = (int)Math.Ceiling(totalSamplesReal);
            List<double> sampleList = new List<double>(totalSamples);
            List<List<double>> preSampleList = new List<List<double>>(totalSamples);
            for (int i = 0; i < totalSamples; i++) {
                sampleList.Add(0.0);
                preSampleList.Add(new List<double>());
            }
            double pointer = 0;
            double step = length;
            double waveLength = waveForm.Length*length;
            for (int repetition = 0; repetition < repetitions; repetition++) {
                for (int waveFormIndex = 0; waveFormIndex < waveForm.Length; waveFormIndex++) {
                    for (double sampleIndex = pointer; sampleIndex < pointer+step; sampleIndex++) {
                        preSampleList[(int)sampleIndex].Add(waveForm[waveFormIndex]);
                    }
                    pointer+=step;
                }
            }
            for (int sampleIndex = 0; sampleIndex < totalSamples; sampleIndex++) {
                foreach (double sampleBit in preSampleList[sampleIndex]) {
                    sampleList[sampleIndex] += sampleBit;

                }
                sampleList[sampleIndex] /= preSampleList[sampleIndex].Count;
            }
            return sampleList;
        }
    }
}
