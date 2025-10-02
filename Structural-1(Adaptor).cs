using System;

namespace AdapterPatternDemo
{
    public interface IMediaPlayer
    {
        void Play(string filename);
    }

    public class AudioPlayer : IMediaPlayer
    {
        public void Play(string filename)
        {
            if (filename.EndsWith(".mp3"))
                Console.WriteLine($"Playing {filename} in AudioPlayer");
            else
                Console.WriteLine("Cannot play this format");
        }
    }

    public class MediaAdapter : IMediaPlayer
    {
        private readonly string filename;
        public MediaAdapter(string filename) => this.filename = filename;

        public void Play(string filename = null) =>
            Console.WriteLine($"Adapter converting and playing {this.filename}");
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Structural-1 (Adaptor)");
            var audioPlayer = new AudioPlayer();
            audioPlayer.Play("song.mp3");

            var adapter = new MediaAdapter("video.mp4");
            adapter.Play();
        }
    }
}
