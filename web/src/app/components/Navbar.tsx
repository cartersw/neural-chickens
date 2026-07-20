import Link from 'next/link'
import React from 'react'
import { RiNextjsFill } from 'react-icons/ri'

const Navbar = () => {
    return (
        <nav className='fixed top-0 w-full flex items-center justify-around py-5 px-24 border
        -b border-gray-700 bg-black'>

        <Link href="/" className="transition duration-300 hover:scale-110">
            <RiNextjsFill className="w-16 h-16"></RiNextjsFill>
            </Link>

            <ul className="flex gap-10 text-lg">
                <Link href="/about" className="text-gray-300 hover:text-white transition-colors">About</Link>
                <Link href="/test" className="text-gray-300 hover:text-white transition-colors">Test</Link>
            </ul>
        </nav>

        
    )
}

export default Navbar