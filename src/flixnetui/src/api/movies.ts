import api from './axios'

export async function getAllMovies() {
    const response = await api.get('/movies')
    return response.data.data
}
