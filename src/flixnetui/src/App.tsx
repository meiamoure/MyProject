import * as React from 'react';
import { useEffect, useState } from 'react';
import { getAllMovies } from './api/movies'
import './App.css'


interface Genre {
    id: string;
    genreName: string;
}

interface Movie {
    id: string;
    title: string;
    description: string;
    posterUrl: string;
    genres?: Genre[];
}

interface User {
    id: string;
    name: string;
    email: string;
    pictureUrl: string;
}

// Все жанры, включая «All»
const GENRES = [
    'All',
    'Action',
    'Adventure',
    'Drama',
    'Sci-Fi',
    'Fantasy',
    'Horror',
    'Romance',
    'Thriller',
    'Mystery',
    'Animation',
    'Documentary',
    'Crime',
    'Comedy',
    'Biography',
    'Family',
    'Musical',
    'War',
    'Western',
    'History',
    'Sport',
] as const;

type GenreName = typeof GENRES[number];

export default function App() {
    const [movies, setMovies] = useState<Movie[]>([]);
    const [search, setSearch] = useState('');
    const [selectedGenre, setSelectedGenre] = useState<GenreName>('All');
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState<User | null>(null);

    // — загрузка профиля
    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const token = params.get('token');

        async function loadUser(jwt: string) {
            try {
                const res = await fetch('/api/users/me', {
                    headers: { Authorization: `Bearer ${jwt}` },
                });
                if (!res.ok) throw new Error();
                const data: User = await res.json();
                setUser(data);
                setIsAuthenticated(true);
            } catch {
                localStorage.removeItem('jwt');
                setIsAuthenticated(false);
            }
        }

        if (token) {
            localStorage.setItem('jwt', token);
            loadUser(token);
            window.history.replaceState({}, '', '/');
        } else {
            const saved = localStorage.getItem('jwt');
            if (saved) loadUser(saved);
        }
    }, []);

    // — загрузка фильмов
    useEffect(() => {
        getAllMovies().then(res => {
            setMovies(res as unknown as Movie[])
        })
            .catch(console.error);
    }, []);

    // — фильтрация
    const filteredMovies = movies.filter(m => {
        // по поиску
        const bySearch = m.title.toLowerCase().includes(search.toLowerCase());
        // по жанру
        const byGenre =
            selectedGenre === 'All' ||
            // m.genres может быть undefined, поэтому ?. и fallback []
            m.genres?.some(g => g.genreName === selectedGenre);

        return bySearch && byGenre;
    });

    function handleLogin() {
        window.location.href = 'https://localhost:7007/api/auth/signin-google';
    }

    return (
        <div className="app-wrapper">
            <div className="app-container">
                {/* Header */}
                <header className="header">
                    <input
                        type="text"
                        placeholder="Search"
                        value={search}
                        onChange={e => setSearch(e.target.value)}
                    />
                    <div className="user-area">
                        {isAuthenticated && user ? (
                            <>
                                <img src={user.pictureUrl} alt="avatar" />
                                <span>{user.name}</span>
                                <button
                                    onClick={() => {
                                        localStorage.removeItem('jwt');
                                        setIsAuthenticated(false);
                                        setUser(null);
                                    }}
                                >
                                    LogOut
                                </button>
                            </>
                        ) : (
                            <button onClick={handleLogin}>Sign In</button>
                        )}
                    </div>
                </header>

                {/* Content */}
                <div className="main">
                    {/* Sidebar */}
                    <aside className="sidebar">
                        <h2>Genres</h2>
                        <ul>
                            {GENRES.map(g => (
                                <li
                                    key={g}
                                    className={g === selectedGenre ? 'active' : ''}
                                    onClick={() => setSelectedGenre(g)}
                                >
                                    {g}
                                </li>
                            ))}
                        </ul>
                    </aside>

                    {/* Movies */}
                    <section className="movies">
                        <h1>
                            {selectedGenre === 'All' ? 'All Movies' : selectedGenre + ' Movies'}
                        </h1>
                        <div className="grid">
                            {filteredMovies.map(m => (
                                <div key={m.id} className="card">
                                    <img src={m.posterUrl || '/placeholder.jpg'} alt={m.title} />
                                    <div className="card-title">{m.title}</div>
                                </div>
                            ))}
                        </div>
                    </section>
                </div>
            </div>
        </div>
    );
}